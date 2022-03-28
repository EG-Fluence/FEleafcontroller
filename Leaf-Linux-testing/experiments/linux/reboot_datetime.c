
#include<stdio.h>
#include<time.h>

// crontab:
// @reboot /logs/reboot_datetime
//
// timedatectl

char s [100];

int main()
{
    time_t t;

    FILE *out = fopen ( "/logs/reboot_datetime.log", "a" );

    time ( &t );

    fputs ( "+++++++++++++++++++++++++++++++++++++++++\n\r", out );

    fputs ( "Reboot datetime: ", out );

    sprintf ( s, "%s", ctime(&t) );

    fputs ( s, out );

    fputs ( "-----------------------------------------\n\r\n\r", out );

	fclose ( out );

    return 0;
}

