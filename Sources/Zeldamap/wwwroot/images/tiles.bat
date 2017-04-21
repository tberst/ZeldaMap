@echo off

echo should already have original image in folder, as well as folders named tiles and samples

set basename=BotW-Map-FULL_BW
set filename=BotW-Map-FULL_BW.png
set extension=png

set imagemagick=magick
REM C:\path\to\ImageMagick\convert
set /a tilesize=256
set /a samplesize=500

set tilesfolder=tiles
set samplesfolder=samples

echo create tile folders
mkdir %tilesfolder%\%basename%
REM mkdir %tilesfolder%\%basename%\1000
REM mkdir %tilesfolder%\%basename%\500
REM mkdir %tilesfolder%\%basename%\250
REM mkdir %tilesfolder%\%basename%\125
REM mkdir %tilesfolder%\%basename%\62
mkdir %tilesfolder%\%basename%\32
mkdir %tilesfolder%\%basename%\16
mkdir %tilesfolder%\%basename%\8
mkdir %tilesfolder%\%basename%\4
mkdir %tilesfolder%\%basename%\2
mkdir %tilesfolder%\%basename%\1





echo create half-sized versions for tiling (will be discarded later)

REM %imagemagick% %filename% -resize 50%%  %basename%-500.%extension%
REM %imagemagick% %filename% -resize 25%%  %basename%-250.%extension%
REM %imagemagick% %filename% -resize 12.5%%  %basename%-125.%extension%
REM %imagemagick% %filename% -resize 6.25%%  %basename%-62.%extension%
%imagemagick% %filename% -resize 3.125%%  %basename%-32.%extension%
%imagemagick% %filename% -resize 1.5625%%  %basename%-16.%extension%
%imagemagick% %filename% -resize 0.78125%%  %basename%-8.%extension%
%imagemagick% %filename% -resize 0.390625%%  %basename%-4.%extension%
%imagemagick% %filename% -resize 0.1953125%%  %basename%-2.%extension%
%imagemagick% %filename% -resize 0.09765625%%  %basename%-1.%extension%



echo create sample
%imagemagick% %filename% -thumbnail %samplesize%x%samplesize%  ./%samplesfolder%/%filename%

echo create tiles
REM %imagemagick% %filename% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/1000/%%[filename:tile].%extension%"
REM %imagemagick% %basename%-500.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/500/%%[filename:tile].%extension%"
REM %imagemagick% %basename%-250.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/250/%%[filename:tile].%extension%"
REM %imagemagick% %basename%-125.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/125/%%[filename:tile].%extension%"
REM %imagemagick% %basename%-62.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/62/%%[filename:tile].%extension%"

%imagemagick% %basename%-32.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/32/%%[filename:tile].%extension%"
%imagemagick% %basename%-16.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/16/%%[filename:tile].%extension%"
%imagemagick% %basename%-8.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/8/%%[filename:tile].%extension%"
%imagemagick% %basename%-4.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/4/%%[filename:tile].%extension%"
%imagemagick% %basename%-2.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/2/%%[filename:tile].%extension%"
%imagemagick% %basename%-1.%extension% -crop %tilesize%x%tilesize% -set filename:tile "%%[fx:page.x/%tilesize%]_%%[fx:page.y/%tilesize%]" +repage +adjoin "./%tilesfolder%/%basename%/1/%%[filename:tile].%extension%"

echo cleanup
REM del %basename%-500.%extension%
REM del %basename%-250.%extension%
REM del %basename%-125.%extension%
REM del %basename%-62.%extension%
del %basename%-32.%extension%
del %basename%-16.%extension%
del %basename%-8.%extension%
del %basename%-4.%extension%
del %basename%-2.%extension%
del %basename%-1.%extension%

echo DONE
pause