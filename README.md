## Timelapse Lite
#
Record IP camera in steps

#

To compile with mono
<br>

    $ xbuild /p:Configuration=Release TimelapseLite.sln
To run with mono

    $ cd TimelapseLite/TimelapseLite/bin/Release
    $ mono TimelapseLite.exe
#
To export videos you will need ffmpeg,

Windows you can get it right [here](https://ffmpeg.org/download.html).
<br>
for xp [here](https://rwijnsma.home.xs4all.nl/files/ffmpeg/ffmpeg-4.5-697-5541cff-win32-static-xpmod-sse.7z).
<br><br>
Or on Debian, Ubuntu or Raspbian

    $ sudo apt-get install ffmpeg


Or on mac

    $ brew install ffmpeg
#

[Windows XP Images](repo/blob/main/images/XP/README.md).

[Windows 10 Images](repo/blob/main/images/10/README.md).

[Linux Lite Images](repo/blob/main/images/LinuxLite/README.md).
