#/bin/bash
####################################################################################
#
# Generate ip address
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-10-11
# NOTE: Following packets must be installed:
#    
# You will need write permissions in the directory where you executing this script
#
####################################################################################

SCRIPTFILENAME=`which $0`
SCRIPTNAME=${SCRIPTFILENAME##*/}
SCRIPTDIR=${SCRIPTFILENAME%/*}


localhelp () {
  echo "Usage:"
  echo "  ${SCRIPTNAME}"
  echo "    Generate IP Gen6 schema"
  echo "  ${SCRIPTNAME} --cores Breakers Feeders Cores Nodes"
  echo "  ${SCRIPTNAME} --cores Breakers Feeders Cores Nodes OutputFile"
  echo "  ${SCRIPTNAME} --cubes Breakers Feeders Cores Nodes Rows Cubes"
  echo "  ${SCRIPTNAME} --cubes Breakers Feeders Cores Nodes Rows Cubes OutputFile"
  echo "  ${SCRIPTNAME} --all Breakers Feeders Cores Nodes Rows Cubes"
  echo "  ${SCRIPTNAME} --all Breakers Feeders Cores Nodes Rows Cubes OutputFile"  
  echo "  ${SCRIPTNAME} help|--help|-h"
  echo "    print help"  
}

PrintCubes=False
PrintCores=False
for var in "$@"
do
  #echo "$var"
  shift # remove argument from arg list
  if [[ "${var}" == "--all" ]]; then
    PrintCores=True
    PrintCubes=True
  elif [[ "${var}" == "--cores" ]]; then
    PrintCores=True
  elif [[ "${var}" == "--cubes" ]]; then
    PrintCubes=True
  else
    set -- "$@" "$var" # add the argument back as the last if is not one of above
  fi
done


for var in "$@"
do
  #echo "$var"
  if [[ "${var}" == "help" || "${var}" == "--help" || "${var}" == "-h" ]]; then
    localhelp
    exit 0
  fi
  #if [ "${var}" == "clear" ]; then
  #  clearData
  #fi
  minOneVarFound=1  # IP, password, help
done



BREAKERS=$1
FEEDERS=$2
CORES=$3
NODES=$4

if [ -z "$BREAKERS" ]; then
  echo "No of breakers is missing"
  localhelp 
  exit 1
fi

if [ -z "$FEEDERS" ]; then
  echo "No of feeders is missing"
  localhelp 
  exit 1
fi

if [ -z "$CORES" ]; then
  echo "No of cores is missing"
  localhelp 
  exit 1
fi

if [ -z "$NODES" ]; then
  echo "No of nodes is missing"
  localhelp 
  exit 1
fi

if [[ ${PrintCores} == "True" ]] ; then
  if [[ -n "$5" ]]; then
    FILE=$5
    rm $FILE >/dev/null
    touch $FILE
  fi
fi


if [[ ${PrintCubes} == "True" ]] ; then
  ROWS=$5
  CUBES=$6
  FILE=$7
  if [ -z "$ROWS" ]; then
    echo "No of cubes is missing"
    localhelp 
    exit 1
  fi  
  if [ -z "$CUBES" ]; then
    echo "No of cubes is missing"
    localhelp 
    exit 1
  fi
  
  if [[ -n "$7" ]]; then
    FILE=$7
    rm $FILE  >/dev/null
    touch $FILE
  fi
else
  ROWS=0
  CUBES=0
fi

#unset ALL_PROXY

Core=0
for ((breaker = 1 ; breaker <= $BREAKERS ; breaker++)); do
  echo "Breaker: $breaker"
  for ((feeder = 1 ; feeder <= $FEEDERS ; feeder++)); do
    echo "  Feeder: $feeder"
    for ((core = 1 ; core <= $CORES ; core++)); do
      ##C=$(($C+1))
      ((Core++))
      OCTET2=$(($breaker * 10 + $feeder))
      OCTET3=$((8 * ${core}))
      echo "    Core: $Core  IP: 10.$OCTET2.$OCTET3.3"
      echo "      CTR Controller  IP: 10.$OCTET2.$OCTET3.161"
      [ $PrintCores == "True" ] && [ -f "$FILE" ] && echo "10.$OCTET2.$OCTET3.3" >> "$FILE" && echo "10.$OCTET2.$OCTET3.161" >> "$FILE"
      for ((node = 1 ; node <= $NODES ; node++)); do
        OCTET3=$((8 * ${core} + $node))
        echo "      Node: $node  IP: 10.$OCTET2.$OCTET3.3"
        [ $PrintCores == "True" ] && [ -f "$FILE" ] && echo "10.$OCTET2.$OCTET3.3" >> "$FILE"
        for ((row = 1 ; row <= $ROWS ; row++)); do
          for ((cube = 1 ; cube <= $CUBES ; cube++)); do
            OCTET4=$((100 * ${row} + $cube))
            echo "        Cube: $cube  IP: 10.$OCTET2.$OCTET3.$OCTET4"
            [ $PrintCubes == "True" ] && [ -f "$FILE" ] && echo "10.$OCTET2.$OCTET3.$OCTET4" >> "$FILE"
            #echo "
          done
        done
      done
    done
  done
done
