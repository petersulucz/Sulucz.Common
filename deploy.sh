#!/bin/bash

if [ "$TRAVIS_BUILD_NUMBER" == "" ]; then
	TRAVIS_BUILD_NUMBER=0
fi

AssemblyVersion="1.0.$TRAVIS_BUILD_NUMBER"

if [ "$TRAVIS_BRANCH" != "master" ]; then
	VersionSuffix=""
else
	VersionSuffix="$TRAVIS_PULL_REQUEST_BRANCH"
fi

echo "Build number: $TRAVIS_BUILD_NUMBER"
echo "Assembly version $AssemblyVersion"
echo "Version suffix $VersionSuffix"

# Now find all of the directories with a nuspec...
NugetProjectFolders=$(find `pwd` -regex .*.*\.nuspec | grep -v '/obj/' | grep -v '/bin/')
for nuspec in $NugetProjectFolders
do
	projdir=$(dirname $nuspec)

	echo "Packing project $projdir"

	dotnet pack --version-suffix "'$VersionSuffix'" /p:version=$AssemblyVersion $projdir
	if [ $? -ne 0 ]; then
		exit 1
	fi
done

# Now find all of the nuget packages.
#NugetPackages=$(find `pwd` -regex .*Sulucz\..*.*\.nupkg | grep -v '/obj/')
#for package in $NugetPackages
#do
#	echo "Pushing package $package"
#	
#	dotnet nuget push --api-key "$NUGET_API_KEY" --source 'https://www.nuget.org' $package
#	if [ $? -ne 0 ]; then
#		exit 1
#	fi
#done