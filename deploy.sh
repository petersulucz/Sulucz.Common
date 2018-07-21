#!/bin/bash
AssemblyVersion="1.0.$TRAVIS_BUILD_NUMBER"

if [ "$TRAVIS_BRANCH" != "master" ]; then
	VersionSuffix=""
else
	VersionSuffix="$TRAVIS_PULL_REQUEST_BRANCH"
fi

echo "Assembly version $AssemblyVersion"
echo "Version suffix $VersionSuffix"

# Now find all of the directories with a nuspec...
NugetProjectFolders=$(find `pwd` -regex .*.*\.nuspec | grep -v '/obj/' | grep -v '/bin/')
for project in $NugetProjectFolders
do
	dotnet pack --version-suffix "'$VersionSuffix'" /p:version=$AssemblyVersion $project
	if [ $? -ne 0 ]; then
		exit $?
	fi
done

# Now find all of the nuget packages.
NugetPackages=$(find `pwd` -regex .*Sulucz\..*.*\.nupkg | grep -v '/obj/')
for package in $NugetPackages
do
	echo "Pushing package $package"
	dotnet nuget push --api-key "$NUGET_API_KEY" --source 'https://www.nuget.org' $package
	if [ $? -ne 0 ]; then
		exit $?
	fi
done