#!/bin/bash
cp ~/Spiele/FEZ/{FezEngine.orig.dll,ContentSerialization.dll,MonoGame.Framework.dll,SDL2-CS.dll} ./; mv FezEngine.orig.dll FezEngine.dll
for file in *.dll; do
  mono --debug ../FmbLibStripper/bin/Debug/FmbLibStripper.exe $file
done
#cp ~/Spiele/FEZOLD/{MonoGame.Framework.dll,SDL2#.dll} ./

