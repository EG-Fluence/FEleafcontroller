/******************************************************************************
 *                        Independant Run Loop
 ******************************************************************************
 * AUTHOR: Frederic-Philippe Metz, (C) Copyright Frederic-Philippe Metz, 2014
 * DESCRIPTION:
 *     The run loop.
 * CHANGELOG:
 *     2014/09/15, fpm, first initial version
 * TODO:
 ******************************************************************************
 * */
#include <stdio.h>
#include <stdlib.h>
#include <sys/time.h>
#include <sys/types.h>
#include <string.h>
#include <unistd.h>
#include <errno.h>
#include "Log.hxx"
#include "run_loop.h"

namespace runloop {

struct CB_list {
        int fd;
        callback cb;
        void* data;
        CB_list* next;
        int deleted;
};

struct Run_loop_data {
        fd_set rfds;
        struct timeval tv;
        CB_list* first;
        CB_list* last;
        cb_timeout cbto;
        void* to_data;
        cb_error cberr;
        void* err_data;
        int maxfd;
} run_loop_data;

void run_loop_init (cb_timeout cbtimout, cb_error cberr, int to_sec, int to_usec) {
    FD_ZERO(&run_loop_data.rfds);
    run_loop_data.first = 0;
    run_loop_data.last = 0;
    run_loop_data.cbto = cbtimout;
    run_loop_data.cberr = cberr;
    run_loop_data.tv.tv_sec = to_sec;
    run_loop_data.tv.tv_usec = to_usec;
    run_loop_data.maxfd = 0;
}

void run_loop_init (cb_timeout cbtimout, void* to_data, cb_error cberr, void* err_data, int to_sec, int to_usec) {
    FD_ZERO(&run_loop_data.rfds);
    run_loop_data.first = 0;
    run_loop_data.last = 0;
    run_loop_data.cbto = cbtimout;
    run_loop_data.to_data = to_data;
    run_loop_data.cberr = cberr;
    run_loop_data.err_data = err_data;
    run_loop_data.tv.tv_sec = to_sec;
    run_loop_data.tv.tv_usec = to_usec;
    run_loop_data.maxfd = 0;
}

void run_loop_close () {
    FD_ZERO(&(run_loop_data.rfds));
    if (!run_loop_data.first)
        return;
    CB_list* cur = run_loop_data.first;
    CB_list* curnext = run_loop_data.first->next;
    while (cur) {
        free (cur);
        cur = curnext;
        if (cur) {
            curnext = cur->next;
        }
    }
}

void run_loop_add_fd (int fd, callback cb, void* data) {
    FD_SET(fd, &(run_loop_data.rfds));
    if (!run_loop_data.first) {
        run_loop_data.first = (CB_list*)malloc (sizeof(struct CB_list));
        //memset(&(run_loop_data.first),0,sizeof(struct CB_list));
        run_loop_data.first->next = 0;
        run_loop_data.first->deleted = 0;
        run_loop_data.last = run_loop_data.first;
    } else {
        run_loop_data.last->next = (CB_list*)malloc (sizeof(struct CB_list));
        run_loop_data.last->next->deleted = 0;
        //memset(&(run_loop_data.last->next),0,sizeof(struct CB_list));
        run_loop_data.last = run_loop_data.last->next;
    }
    run_loop_data.last->next = 0;
    run_loop_data.last->fd = fd;
    run_loop_data.last->cb = cb;
    run_loop_data.last->data = data;
    run_loop_data.maxfd = (fd>run_loop_data.maxfd)?fd:run_loop_data.maxfd;
}

void run_loop_deletionmark_fd(int fd) {
    CB_list* cur = run_loop_data.first;
    while (cur) {
        if (cur->fd == fd) {
        	mdebug("Setting deletion mark for fd: "+toString(cur->fd));
        	cur->deleted = 1;
        	break;
        }
        cur = cur->next;
    }

}

void run_loop_sanitize() {

    if (!run_loop_data.first)
        return;

    CB_list* curprevious = run_loop_data.first;
    CB_list* cur = run_loop_data.first;

    // --- if it's the firstone
    if (cur->deleted) {
    	run_loop_data.first = cur->next;
    	free(cur);
    	return;
    }

	cur = cur->next;

    while (cur) {
    	if (cur->deleted) {
    		curprevious->next = cur->next;
        	mdebug("Removed fd: "+toString(cur->fd));
        	if (cur == run_loop_data.last) {
        		run_loop_data.last = curprevious;
        	}
        	close(cur->fd);
    		free(cur); cur=0;
    		break;
    	}
    	curprevious = cur;
    	cur = cur->next;
    }
}

void run_loop_run () {

	run_loop_sanitize();

    FD_ZERO(&(run_loop_data.rfds));
    CB_list* cur = run_loop_data.first;
    while (cur) {
        FD_SET(cur->fd,&(run_loop_data.rfds));
        cur = cur->next;
    }

    // --- don't rely on the value of tv after select, so we copy
    struct timeval tv = run_loop_data.tv;

    int retval = select (run_loop_data.maxfd+1, &(run_loop_data.rfds), NULL, NULL, &tv);

    if ((retval == -1) && (errno == EINTR)) {
        perror ("error");
        run_loop_data.cberr(&run_loop_data.err_data);

    } else if (retval==0) {
        // perror ("timeout");
        run_loop_data.cbto(&run_loop_data.to_data);

    } else {
        // printf("Data there.\n");
        cur = run_loop_data.first;
        while (cur) {
            if (FD_ISSET(cur->fd,&(run_loop_data.rfds))) {
                cur->cb(cur->fd, cur->data);
                break;
            }
            cur = cur->next;
        }
    }
}

void run_loop_get_all_descriptors(int* array) {
	CB_list* cur = run_loop_data.first;
	while (cur) {
		*array = cur->fd;
		++array;
		cur = cur->next;
	}
}

void run_loop_get_all_descriptors_with_data(int array[][2]) {
	CB_list* cur = run_loop_data.first;
	for (int i=0;;++i) {
		array[i][0] = cur->fd;
		array[i][1] = (intptr_t)cur->data;
		cur = cur->next;
		if (!cur) break;
	}
}

}
