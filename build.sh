﻿#!/bin/bash
projects=$(find `pwd` -regex .*\.csproj$)
echo "Compiling projects:"
echo "$projects"

echo "Restoring projects"
for project in $projects 
do
	echo "Restore for $project"
	dotnet restore $project
done

echo "Building projects"
for project in $projects 
do
	echo "Build for $project"
	dotnet build $project
done

AssemblyVersion="1.0.$TRAVIS_BUILD-NUMBER"
VersionSuffix=""

echo "Assembly version $AssemblyVersion"
echo "Version suffix $VersionSuffix"

