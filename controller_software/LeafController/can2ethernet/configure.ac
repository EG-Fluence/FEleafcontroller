# this is example-file: configure.ac

# initial information about the project
AC_INIT([can2ethernet],[0.01],[Frederic.Metz@fluenceenergy.com])

AC_CONFIG_AUX_DIR(config)

# check if the source folder is available
AC_CONFIG_SRCDIR([src/can2ethernet.cpp])

# check for C++ preprocessor and compiler
AC_PROG_CXXCPP
AC_PROG_CXX
AC_PROG_CC

# automake initialization (mandatory) including a check for automake API version >= 1.9
AM_INIT_AUTOMAKE([1.9])

# use the C++ compiler for the following checks
AC_LANG([C++])

# Checks for header files.
AC_HEADER_STDC
AC_CHECK_HEADERS([string])
AC_CHECK_HEADERS([iostream])

# Checks for typedefs, structures, and compiler characteristics.
AC_TYPE_SIZE_T

# distribute additional compiler and linker flags among Makefiles
# --> set and change these variables instead of CXXFLAGS or LDFLAGS (for user only)
AC_SUBST([AM_CXXFLAGS])
AC_SUBST([AM_LDFLAGS])

# files to generate via autotools (prepare .am or .in source files)
#AC_CONFIG_FILES([src/Makefile])

# finally this generates the Makefiles etc. for the build
AC_OUTPUT(Makefile src/Makefile)


