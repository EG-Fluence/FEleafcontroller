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

#ifndef PROCESS_PARAMS_H_
#define PROCESS_PARAMS_H_

#include <map>

struct Params {
        char* can_interface_name;
        char* mcast_ip;
        int mcast_port;
        int verbose_flag;
} ;

void retrieve_params (int argc, char * const argv[], Params* params);

#endif /* PROCESS_PARAMS_H_ */
