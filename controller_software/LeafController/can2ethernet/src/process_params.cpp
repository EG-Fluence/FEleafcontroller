/******************************************************************************
 *                        Parameter processor
 ******************************************************************************
 * AUTHOR: Frederic-Philippe Metz (metz@myfred.net)
 * DESCRIPTION:
 *     Command Line Params stuff
 * CHANGELOG:
 *     2014/09/15, fpm, first initial version
 * TODO:
 ******************************************************************************
 * */


#include <stdio.h>
#include <stdlib.h>
#include <getopt.h>
#include "process_params.h"
#include "Log.hxx"


void print_usage() {
	minfo("----------------------------------------------------------------------");
	minfo("+ can2ethernet usage:");
	minfo("+ can2ethernet <params>");
	minfo("+ Params:");
	minfo("+   -a || --mcast-ip-address : Multicast IP address, i.e. 239.0.0.1");
	minfo("+   -p || --mcast-ip-port    : Multicast IP port, i.e. 6000");
	minfo("+   -c || --can-interface    : can interface name, i.e. can0");
	minfo("+   -h                       : help");
	minfo("+ --------------------------------------------------------------------");
	minfo("+ If you omit <params>, default would be:");
	minfo("+   can2ethernet -a 239.0.0.1 -p 6000 -c can0");
	minfo("----------------------------------------------------------------------");
}

void retrieve_params (int argc, char * const argv[], Params* params) {
    while (1) {
        static struct option long_options[] = {
        /* These options set a flag. */
        { "verbose", no_argument, &params->verbose_flag, 1 },
        { "brief",   no_argument, &params->verbose_flag, 0 },
        /* These options don’t set a flag.
         We distinguish them by their indices. */
        { "can-interface", required_argument, 0, 'c' },
        { "mcast-ip-address", required_argument, 0, 'a' },
        { "mcast-ip-port", required_argument, 0, 'p' },
        { 0, 0, 0, 0 } };
        /* getopt_long stores the option index here. */
        int option_index = 0;

        int c = getopt_long (argc, argv, "hc:a:p:", long_options, &option_index);

        /* Detect the end of the options. */
        if (c == -1)
            break;

        switch (c) {
        case 0:
            /* If this option set a flag, do nothing else now. */
            if (long_options[option_index].flag != 0)
                break;
            if (params->verbose_flag) {
            	minfo ("option %s" + long_options[option_index].name);
                if (optarg)
                	minfo (" with arg %s" + optarg);
                minfo ("\n");
            }
            break;

        case 'a':
            if (params->verbose_flag) {
            	minfo ("option -a with value '%s'\n" + optarg);
            }
            params->mcast_ip = optarg;
            break;

        case 'p':
            if (params->verbose_flag) {
            	minfo ("option -p with value '%s'\n" + optarg);
            }
            params->mcast_port = atoi(optarg);
            break;

        case 'c':
            if (params->verbose_flag) {
            	minfo ("option -c with value '%s'\n" + optarg);
            }
            params->can_interface_name = optarg;
            break;

        case 'h':
            if (params->verbose_flag) {
                minfo ("option -h");
            }
            print_usage();
            exit(0);
            break;
        case '?':
            /* getopt_long already printed an error message. */
            break;
        default:
            return;
        }
    }
}
