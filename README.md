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
<br><br>
Or on Debian, Ubuntu or Raspbian

    $ sudo apt-get install ffmpeg


Or on mac

    $ brew install ffmpeg

