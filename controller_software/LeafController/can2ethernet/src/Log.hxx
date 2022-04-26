// +---------------------------------------------------------------------------
// +  Datei: Log.hxx
// +  AutorIn: Frederic-Philippe Metz
// +  Beschreibung: Logging utilities -> syslog
// +  KorrektorIn:
// +  Revision: 2009/09/19
// +---------------------------------------------------------------------------
#if !defined(LOG_HXX)
#define LOG_HXX
//
// --- Zusätzliche Deklarationen
#include <string>
#include <sstream>
#include <iomanip>
#include <cctype>
#include <syslog.h>
#include <typeinfo>
#include <stdint.h>
#include <algorithm>
#include <stdexcept>

#include "LoggingException.hxx"
//
// --- pragmas zum Test mit icc von Intel (EDG)
#if defined(__EDG__)
#pragma warning (disable:981)  // unspecified order in minfo
#endif

using std::istringstream; using std::string;

#ifndef GITVERSION
#define GITVERSION "TOTAL_DIRTY"
#endif


//
// +---------------------------------------------------------------------------
// +  namespace logging
// +  2009/02/19, fpm
// +---------------------------------------------------------------------------
namespace logging {
//
// +---------------------------------------------------------------------------
// +  operator+(string const&, int), operator+(int, string const&) etc.
// +   nicht als Template wegen "ambiguous overload ..."
// +  2009/05/24, fpm
// +---------------------------------------------------------------------------
inline
std::string operator+(std::string const& s, int const& i) {
    std::ostringstream os; os << i;
    return s + ((os)? (os.str()) : std::string("???"));
}
inline
std::string operator+(int const& i, std::string const& s) {
    std::ostringstream os; os << i;
    return ((os)? os.str() : std::string("???")) + s;
}
inline
std::string operator+(std::string const& s, size_t const& i) {
    std::ostringstream os; os << i;
    return s + ((os)? (os.str()) : std::string("???"));
}
inline
std::string operator+(size_t const& i, std::string const& s) {
    std::ostringstream os; os << i;
    return ((os)? os.str() : std::string("???")) + s;
}
//
// +---------------------------------------------------------------------------
// +  mdump - dump auf syslog bei LOG_DEBUG; mehrere Versionen
// +  2009/08/11, fpm
// +---------------------------------------------------------------------------
inline
void mdump(std::string const& buf) {
    // nur f�r LOG_DEBUG: aktuelle mask holen und wieder setzen
    int mask(setlogmask(0)); setlogmask(mask);
    if (!(mask&LOG_MASK(LOG_DEBUG))) return;
    // Konfiguration: Blockgr��e, F�llzeichen, Trenner zum print-Teil
    size_t const lwidth(16); char const filler('0'); std::string trenner(" | ");
    // blockweise
    for (size_t b(0); b<buf.size(); b+=lwidth) {
        std::ostringstream os; std::string printt;
        // genau ein Block beginnend bei Index b
        size_t i(0);
        for (; i<lwidth && b+i<buf.size(); ++i) {
            os << std::setw(2) << std::setfill(filler) << std::hex
                    << (static_cast<size_t>(buf[b+i]) & 0xff) << ' ';
            printt += (std::isprint(buf[b+i]))? buf[b+i]: '.';
        }
        // am Ende evtl. auff�llen; Trenner setzen
        if (i<lwidth) os << std::string(3*(lwidth-i),' '); os << trenner;
        // Ausgabe
        syslog(LOG_DEBUG, "%s", (os.str() + printt).c_str());
    }
}
inline
void mdump(std::string const& buf,size_t len) {
    // nur f�r LOG_DEBUG: aktuelle mask holen und wieder setzen
    int mask(setlogmask(0)); setlogmask(mask);
    if (!(mask&LOG_MASK(LOG_DEBUG))) return;
    // Konfiguration: Blockgr��e, F�llzeichen, Trenner zum print-Teil
    size_t const lwidth(16); char const filler('0'); std::string trenner(" | ");
    // blockweise
    for (size_t b(0); b<len; b+=lwidth) {
        std::ostringstream os; std::string printt;
        // genau ein Block beginnend bei Index b
        size_t i(0);
        for (; i<lwidth && b+i<len; ++i) {
            os << std::setw(2) << std::setfill(filler) << std::hex
                    << (static_cast<size_t>(buf[b+i]) & 0xff) << ' ';
            printt += (std::isprint(buf[b+i]))? buf[b+i]: '.';
        }
        // am Ende evtl. auff�llen; Trenner setzen
        if (i<lwidth) os << std::string(3*(lwidth-i),' '); os << trenner;
        // Ausgabe
        syslog(LOG_DEBUG, "%s", (os.str() + printt).c_str());
    }
}
// �berladene Versionen f�r die "�blichen" dumps
inline void mdump(char const* p, int l) { mdump(std::string(p,p+l)); }
inline void mdump(char const* p, size_t l) { mdump(std::string(p,p+l)); }
//
/* ----------------------------------------------------------------------------
// fpm, 2009/??/??
inline
std::string char2hexstr(char const& c) {
  unsigned int i = static_cast<unsigned int>(c) & 0xFF;
  std::stringstream ss; ss << std::setfill('0') << std::setw(2) << std::hex << i;
  std::string s; ss >> s;std::string string_to_hex(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();

    std::string output;
    output.reserve(2 * len);
    for (size_t i = 0; i < len; ++i)
    {
        const unsigned char c = input[i];
        output.push_back(lut[c >> 4]);
        output.push_back(lut[c & 15]);
    }
    return output;
}

#include <algorithm>
#include <stdexcept>

std::string hex_to_string(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();
    if (len & 1) throw std::invalid_argument("odd length");

    std::string output;
    output.reserve(len / 2);
    for (size_t i = 0; i < len; i += 2)
    {
        char a = input[i];
        const char* p = std::lower_bound(lut, lut + 16, a);
        if (*p != a) throw std::invalid_argument("not a hex digit");

        char b = input[i + 1];
        const char* q = std::lower_bound(lut, lut + 16, b);
        if (*q != b) throw std::invalid_argument("not a hex digit");

        output.push_back(((p - lut) << 4) | (q - lut));
    }
    return output;
}
  return s;
}
//
inline void mdump(std::string const& s)  {
  static const unsigned int MAXWIDTH=16*3;
  static std::string normedstr, newstr;
  for (unsigned int i=0; i<s.length();++i) { \

    newstr    += char2hexstr(s[i]) + " "; \
    normedstr += ((static_cast<unsigned int>(s[i]) & 0xFF)<32) || \
                 ((static_cast<unsigned int>(s[i]) & 0xFF)>126) ?'.':s[i];\
    if (newstr.length()>=MAXWIDTH || i==s.length()-1) { \
      if (MAXWIDTH-newstr.length()>0) { \
	newstr += std::string(MAXWIDTH-newstr.length(),' ');	\
      } \
      newstr += "| " + normedstr;\
      syslog(LOG_DEBUG, "%s", (std::string()+newstr).c_str()); \
      normedstr.clear(); newstr.clear();
    }
  }
}
-----------------------------------------------------------------------------*/
//
} // namespace
//
// +---------------------------------------------------------------------------
// +  das Macro linfo - liefert (__FILE__:__LINE__) als string; als Macro
// +    implementiert, damit __FILE__ und __LINE__ korrekt übernommen werden.
// +    (bei Bedarf erweiterbar um __TIME__ und __DATE__)
// +  2009/05/12, fpm
// +---------------------------------------------------------------------------
#define STRINGIFY(x) #x
#define TOSTRING(x) STRINGIFY(x)
// #define AT __FILE__ ":" TOSTRING(__LINE__)

#define cinfo \
        std::string(" (") + std::string(__FILE__) + std::string(" - version:") + std::string(VERSION) + std::string(") ")

#define linfo \
        std::string(" (") + std::string(__FILE__) +  std::string(":")+\
                TOSTRING(__LINE__)+std::string(") ")

#define mThrowSt(arg)    { throw (std::string()+arg+linfo); }
#define mThrow(arg)    { merr(arg); throw LoggingException((std::string()+arg+linfo)); }
#define mRaise(arg1,arg2) { merr(arg2); arg1.add((std::string()+arg2+linfo)); throw arg1; }

//
// +---------------------------------------------------------------------------
// +  die syslog Wrapper - Hinweis: s nicht Klammern, falls + verwendet wird!
// +    Als Macros definiert zur Erzeugung von strings auch aus elementaren
// +    Typen, für die kein operator+ definierbar ist - zB: char const* + int
// +  2009/08/11, fpm
// +---------------------------------------------------------------------------
#define merr(s) { syslog(LOG_ERR,      "[ERROR]   %s", (std::string()+s+linfo).c_str()); }
#define mwarn(s) { syslog(LOG_WARNING, "[WARNING] %s", (std::string()+s+linfo).c_str()); }
#define mnote(s) { syslog(LOG_NOTICE,  "[NOTICE]  %s", (std::string()+s).c_str()); }
#define minfo(s) { syslog(LOG_INFO,    "[INFO]    %s", (std::string()+s).c_str()); }
#define mdebug(s) { syslog(LOG_DEBUG,  "[DEBUG]   %s", (std::string()+s+linfo).c_str()); }

inline
void minfoL(std::string const& str) {
    std::string str2 ("\n");
    size_t lastpos=0, found=0;

    // different member versions of find in the same order as above:
    bool end=false;
    while (!end) {
        found=str.find(str2,lastpos);
        if (found==std::string::npos) {
            syslog(LOG_INFO, "%s", (str.substr(lastpos)).c_str());
            end=true;
        } else {
            syslog(LOG_INFO, "%s", (str.substr(lastpos,found-lastpos)).c_str());
            lastpos = found+1; found+=1;
        }
    }
}

inline
std::string hex(uint64_t num) {
    std::ostringstream os;
    {
        os << std::hex << num << std::dec;
    }
    return os.str();
}

inline
std::string hex(short unsigned int num) {
    std::ostringstream os;
    {
        os << std::hex << num << std::dec;
    }
    return os.str();
}

inline
std::string toString(uint64_t num) {
    std::ostringstream os;
    {
        os << num;
    }
    return os.str();
}

inline
uint64_t toNumber(string& num) {
    uint64_t value=0;
    istringstream is(num);
    {
        is >> value;
    }
    return value;
}

inline
uint64_t hexToNumber(string& num) {
    uint64_t value=0;
    istringstream is(num);
    {
        is >> std::hex >> value;
    }
    return value;
}

inline
void str2bcd(std::string amount, char* result, size_t len) {
    char* cur = result+len-1;
    bool rightnibble = true;
    for (std::string::reverse_iterator rit=amount.rbegin(); rit!=amount.rend(); ++rit) {
        char c = *rit - '0';
        if (rightnibble) {
            *cur = c;
            rightnibble = false;
        } else {
            *cur |= c << 4;
            --cur;
            rightnibble = true;
        }
    }
}

inline void bcd2str(string& amount, const char* data, size_t len) {
    for (int i=0; i<len; ++i) {
        char leftnibble = ((data[i] >> 4) & 0x0f) + '0';
        amount += leftnibble;
        char rightnibble = ((data[i]) & 0x0f) + '0';
        amount += rightnibble;
        leftnibble = rightnibble = 0;
    }
}

inline
std::string string_to_hex(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();

    std::string output;
    output.reserve(2 * len);
    for (size_t i = 0; i < len; ++i)
    {
        const unsigned char c = input[i];
        output.push_back(lut[c >> 4]);
        output.push_back(lut[c & 15]);
    }
    return output;
}

inline
std::string hex_to_string(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();
    if (len & 1) throw std::invalid_argument("odd length");

    std::string output;
    output.reserve(len / 2);
    for (size_t i = 0; i < len; i += 2)
    {
        char a = input[i];
        const char* p = std::lower_bound(lut, lut + 16, a);
        if (*p != a) throw std::invalid_argument("not a hex digit");

        char b = input[i + 1];
        const char* q = std::lower_bound(lut, lut + 16, b);
        if (*q != b) throw std::invalid_argument("not a hex digit");

        output.push_back(((p - lut) << 4) | (q - lut));
    }
    return output;
}
//
#endif
//
// --- Log Priority is logged up to, rest is thrown away
// --- LOG_EMERG
// ---    The message says the system is unusable.
// --- LOG_ALERT
// ---    Action on the message must be taken immediately.
// --- LOG_CRIT
// ---    The message states a critical condition.
// --- LOG_ERR
// ---    The message describes an error.
// --- LOG_WARNING
// ---    The message is a warning.
// --- LOG_NOTICE
// ---    The message describes a normal but important event.
// --- LOG_INFO
// ---    The message is purely informational.
// --- LOG_DEBUG
// ---    The message is only for debugging purposes.
//  Ende der Datei
//
