#!/bin/bash
cp ~/Spiele/FEZ_NODRM_111/{FezEngine.orig.dll,ContentSerialization.dll,MonoGame.Framework.dll,SDL2-CS.dll,Common.orig.dll} ./; mv FezEngine.orig.dll FezEngine.dll; mv Common.orig.dll Common.dll
for file in *.dll; do
  mono --debug ../FmbLibStripper/bin/Debug/FmbLibStripper.exe $file
done
#cp ~/Spiele/FEZOLD/{MonoGame.Framework.dll,SDL2#.dll} ./

