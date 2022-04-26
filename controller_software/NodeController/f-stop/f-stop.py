#!/usr/bin/python
#
import sys, getopt

import os
import fluencelog
logger = fluencelog.initlog()
import Holding_Read
import Holding_Write
import Input_Read
import pymodbus
from pymodbus.exceptions import ConnectionException

DEST_IP="127.0.0.1"
DEST_PORT="1502"
DEST_FSTOP_REGISTER=6100
DEST_FSTOP_LOCATION_REGISTER=6102
DEST_FSTOP_WIRINGERROR_LOCATION_REGISTER=6184
DEST_FSTOP_CORE_CODE_ERROR=6186

class State:
    NEUTRAL = 0
    CASE1LATCHED  = 1

# for testing purpose without config server retrieval
def getFileCubeNodeIps(ipPortTupleList, filename):

    try:
        f = open(filename)
        Lines = f.readlines()
        for line in Lines:
            ipPortList = line.split(":");
            ipPortTuple = (ipPortList[0], ipPortList[1])
            ipPortTupleList.append(ipPortTuple)
    except IOError:
        logger.info("No connection data available. Fill 'connection_data.conf' with <IP:PORT> lines of cubes or nodes")
    finally:
        f.close()

def runNodeController():
    # get the IPs
    ipPortTupleList = []
    getFileCubeNodeIps(ipPortTupleList, "cubes.conf")

    cubeId=0
    cubeFstopFailurePosition=0
    cubeFstopWiringErrorPosition=0

    for item in ipPortTupleList:
        logger.info( "--------------------------------------------------------------------------------")
        port = item[1].rstrip("\n")
        logger.info( "Connecting to Cube idx " + str(cubeId) + ": " + item[0] + " on Port : " + port)
        try:
            fStopK1 = Input_Read.Input_Read(item[0], port, "8", "42", "16int", "0", "1", "0");
            fStopButton = Input_Read.Input_Read(item[0], port, "9", "42", "16int", "0", "1", "0");
        except pymodbus.exceptions.ConnectionException:
            logger.error( "Connection to Cube idx " + str(cubeId) + ": " + item[0] + " on Port : " + port + "failed.")
            cubeId+=1 // TODO: What do we do here ??
            continue
        logger.info("Got F-Stop K1:" + str(fStopK1) + " and F-Stop Button:" + str(fStopButton))
        if fStopK1 == 0 and fStopButton == 0:
            cubeFstopFailurePosition |= 1 << cubeId
        if fStopK1 == 1 and fStopButton == 0:
            cubeFstopWiringErrorPosition |= 1 << cubeId
        cubeId+=1
        
    logger.info("Result: Cubes Bit Position mask : " + bin(cubeFstopFailurePosition))
    
    try:
        if cubeFstopFailurePosition == 0:
            Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_REGISTER, "16int", "0", "1", "0", "No comment")
        else:
            Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_REGISTER, "16int", "1", "1", "0", "No comment")

        Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_LOCATION_REGISTER, "16int", str(cubeFstopFailurePosition), "1", "0", "No comment")
        Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_WIRINGERROR_LOCATION_REGISTER, "16int", str(cubeFstopWiringErrorPosition), "1", "0", "No comment")
    except pymodbus.exceptions.ConnectionException:
        logger.error( "Writing to local register failed.")
            
def runCoreController(state):
    
    # get the IP(s)
    ipPortTupleList = []
    getFileCubeNodeIps(ipPortTupleList, "nodes.conf")
    ipPortTupleListOCTE = []
    getFileCubeNodeIps(ipPortTupleListOCTE, "octe.conf")
    if len(ipPortTupleListOCTE) != 1:
        logger.error("List of OCTE's can only be 1")
        exit(1)

    nodeId=0
    nodeFstopFailurePosition=0
    
    # read the registers
    for item in ipPortTupleList:
        try:
            regFStopOnNode = Input_Read.Input_Read(item[0], port, str(DEST_FSTOP_REGISTER), "1", "16int", "0", "1", "0");
        except pymodbus.exceptions.ConnectionException:
            logger.error( "Connection to Node idx " + str(cubeId) + ": " + item[0] + " on Port : " + port + "failed.")
            nodeId+=1
            continue
        if regFStopOnNode != 0:
            nodeFstopFailurePosition |= 1 << nodeId
        nodeId+=1
    
    logger.info("Result: Nodes Bit Position mask : " + bin(nodeFstopFailurePosition))
    
    try:
        if nodeFstopFailurePosition == 0:
            Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_REGISTER, "16int", "0", "1", "0", "No comment")
        else:
            Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_REGISTER, "16int", "1", "1", "0", "No comment")

        Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_LOCATION_REGISTER, "16int", str(nodeFstopFailurePosition), "1", "0", "No comment")
    except pymodbus.exceptions.ConnectionException:
        logger.error( "Writing to local register failed.")
                

    failureInFStop = False
    zeroDetected = False
    try:
        logger.info( "--------------------------------------------------------------------------------")
        ip = ipPortTupleListOCTE[0][0]
        port = ipPortTupleListOCTE[0][1].rstrip("\n")
        logger.info( "Connecting to OCTE : " + ip + " on Port : " + port)
        reg4 = Input_Read.Input_Read(ip, port, "4", "42", "16int", "0", "1", "0");
        if reg4 == 0:
            zeroDetected = True
        reg3 = Input_Read.Input_Read(ip, port, "3", "42", "16int", "0", "1", "0");
        if reg3 == 0:
            zeroDetected = True
        if reg3 == 1 and zeroDetected == True:
            failureInFStop = True
        reg5 = Input_Read.Input_Read(ip, port, "5", "42", "16int", "0", "1", "0");
        if reg5 == 0:
            zeroDetected = True
        if reg5 == 1 and zeroDetected == True:
            failureInFStop = True
        reg6 = Input_Read.Input_Read(ip, port, "6", "42", "16int", "0", "1", "0");
        if reg6 == 0:
            zeroDetected = True
        if reg6 == 1 and zeroDetected == True:
            failureInFStop = True
        reg7 = Input_Read.Input_Read(ip, port, "7", "42", "16int", "0", "1", "0");
        if reg7 == 1 and zeroDetected == True:
            failureInFStop = True
        logger.info("Got reg4:" + str(reg4) + " reg3:" + str(reg3) + " reg5:" + str(reg3) + " reg6:" + str(reg3) + " reg7:" + str(reg3) + " regFStopOnNode:" + str(regFStopOnNode))
    except pymodbus.exceptions.ConnectionException:
        logger.error( "Connection to OCTE idx : " + ip + " on Port : " + port + "failed.")

    # normal operation is back again
    if reg4 == 1 and reg3 == 1 and reg5 == 1 and reg6 == 1 and reg7 == 1 and regFStopOnNode == 0:
        try:
            Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "0", "1", "0", "No comment")
            state = State.NEUTRAL
        except pymodbus.exceptions.ConnectionException:
            logger.error( "Writing to local register failed.")

    try:
        if state == State.NEUTRAL:
        # failure in F-Stop circuit alarm
            if failureInFStop == True:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "6", "1", "0", "No comment")

            if reg4 == 1 and reg3 == 1 and reg5 == 1 and reg6 == 0 and reg7 == 0 and regFStopOnNode == 1:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "1", "1", "0", "No comment")
                state = State.CASE1LATCHED

            if reg4 == 1 and reg3 == 1 and reg5 == 1 and reg6 == 1 and reg7 == 0:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "1", "1", "0", "No comment")
                state = State.CASE1LATCHED

            if reg4 == 1 and reg3 == 1 and reg5 == 0 and reg6 == 0 and reg7 == 0 and regFStopOnNode == 0:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "3", "1", "0", "No comment")
                
            if reg4 == 1 and reg3 == 0 and reg5 == 0 and reg6 == 0 and reg7 == 0:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "4", "1", "0", "No comment")

            if reg4 == 0 and reg3 == 0 and reg5 == 0 and reg6 == 0 and reg7 == 0:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "5", "1", "0", "No comment")
                
            if reg7 == 0 and regFStopOnNode == 1:
                Holding_Write.Holding_Write(DEST_IP, DEST_PORT, DEST_FSTOP_CORE_CODE_ERROR, "16int", "2", "1", "0", "No comment")

        else if state == State.CASE1LATCHED:
            pass # only normal operation takes us back to neutral state
        else:
            logger.error( "State does not exist.")
    except pymodbus.exceptions.ConnectionException:
        logger.error( "Writing to local register failed.")

    return state

def main(argv):
    controller=os.environ.get("CONTROLLER", "NOTSET")
    if controller == "NOTSET":
        logger.error("Cannot determine if I run on NMC or CMC since ${CONTROLLER} is not set. Set or source /usr/local/share/fluence/fluence_vars.sh.")
    if controller == "NMC":
        while (True):
            runNodeController()
    if controller == "CMC":
        State state = State.NEUTRAL
        while (True):
            state = runCoreController(state)

if __name__ == "__main__":
    main(sys.argv[1:])
