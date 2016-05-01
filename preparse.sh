#!/bin/bash
BASEDIR=$(pwd)/PreParsedBases
rm -R $BASEDIR
mkdir $BASEDIR

#xbuild FmbLib.Unity.csproj /p:EnablePreParsed='0'
mkdir ${BASEDIR}/UNITY

#xbuild FmbLib.XNAFEZ.csproj /p:EnablePreParsed='0'
mkdir ${BASEDIR}/XNAFEZ

xbuild FmbLibTester/FmbLibTester.csproj /p:EnablePreParsed='0'
pushd FmbLibTester/bin/Debug
mono FmbLibTester.exe -pp ${BASEDIR}/UNITY
popd

xbuild FmbLibTester/FmbLibTester.csproj /p:EnablePreParsed='0' /p:Configuration='Debug - XNAFEZ'
pushd FmbLibTester/bin/DebugXNAFEZ
mono FmbLibTester.exe -pp ${BASEDIR}/XNAFEZ
popd
