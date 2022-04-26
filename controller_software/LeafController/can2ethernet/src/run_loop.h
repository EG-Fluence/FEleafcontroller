/******************************************************************************
 *                        Independant Run Loop
 ******************************************************************************
 * AUTHOR: Frederic-Philippe Metz, (C) Copyright Frederic-Philippe Metz, 2014
 * DESCRIPTION:
 *     The run loop.
 * ATTENTION: The loop is not thread safe !!!
 * CHANGELOG:
 *     2014/09/15, fpm, first initial version
 * TODO:
 ******************************************************************************
 * */

#ifndef RUN_LOOP_H_
#define RUN_LOOP_H_

namespace runloop {

typedef void (*callback) (int fd, void* data);
typedef void (*cb_timeout) (void* to_data);
typedef void (*cb_error) (void* err_data);

void run_loop_init (cb_timeout timout, cb_error cberr, int to_sec, int to_usec);
void run_loop_init (cb_timeout cbtimout, void* to_data, cb_error cberr, void* err_data, int to_sec, int to_usec);
void run_loop_close ();

void run_loop_add_fd(int fd, callback cb, void* data);
void run_loop_deletionmark_fd(int fd);
void run_loop_sanitize(int fd);

void run_loop_run ();

void run_loop_get_all_descriptors(int* array);
void run_loop_get_all_descriptors_with_data(int array[][2]);

}

#endif /* RUN_LOOP_H_ */
