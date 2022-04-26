#include <list>
#include <string>

#ifndef RELOADEXCEPTION_HXX
#define RELOADEXCEPTION_HXX

class LoggingException : public std::exception
{
public:


    LoggingException(std::string str) { s.push_back(str); }

    ~LoggingException() throw() {}

    void add(std::string str) throw() { s.push_back(str); }

    std::string last() throw() { return s.back(); }

    virtual const char* what() const throw() {

        std::string str;
        std::list<std::string>::const_iterator beg = s.begin();
        std::list<std::string>::const_iterator end = s.end();
        while (beg!=end)
        {
            str+= *beg+'\n';
            ++beg;
        }
        return str.c_str();
    }

private:

    LoggingException() {}

    std::list<std::string> s;
};

#endif
