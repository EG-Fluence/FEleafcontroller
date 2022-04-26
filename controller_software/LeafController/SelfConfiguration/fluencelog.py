import sys
import logging
from logging.handlers import SysLogHandler


class LessThanFilter(logging.Filter):
    def __init__(self, exclusive_maximum, name=""):
        super(LessThanFilter, self).__init__(name)
        self.max_level = exclusive_maximum

    def filter(self, record):
        #non-zero return means we log this message
        return 1 if record.levelno < self.max_level else 0


def initlog():

    formatter = logging.Formatter(fmt='%(asctime)s,%(msecs)d %(levelname)-8s [%(filename)s:%(lineno)d] %(message)s',datefmt='%Y-%m-%d:%H:%M:%S')

    #Get the root logger
    logger = logging.getLogger()
    #Have to set the root logger level, it defaults to logging.WARNING
    logger.setLevel(logging.NOTSET)
    logger.handlers = []
    
    syslogHandler = SysLogHandler(address= '/dev/log')
    logger.addHandler(syslogHandler)

    logging_handler_out = logging.StreamHandler(sys.stdout)
    logging_handler_out.setLevel(logging.DEBUG)
    logging_handler_out.addFilter(LessThanFilter(logging.WARNING))
    logging_handler_out.setFormatter(formatter)
    
    logger.addHandler(logging_handler_out)

    logging_handler_err = logging.StreamHandler(sys.stderr)
    logging_handler_err.setLevel(logging.WARNING)
    logging_handler_err.setFormatter(formatter)
    logger.addHandler(logging_handler_err)

    return logger
    

