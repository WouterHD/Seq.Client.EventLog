ECHO --- Build Release
MSBuild.exe src/Seq.Client.EventLog.sln -restore -p:OutDir=../../out -p:Configuration="Release" || exit /b 255

ECHO --- Create Release Packs
octo pack --basePath="out" --id="Seq.Client.EventLog" --version=0.0.0.%BUILDKITE_BUILD_NUMBER% --format=Zip --verbose --outFolder="octo" || exit /b 255
octo push --package Seq.Client.EventLog.0.0.0.%BUILDKITE_BUILD_NUMBER%.zip --project="beheer" --server="%OCTO_HOST%" --apiKey %OCTO_APIKEY%
octo create-release --packageversion="0.0.0.%BUILDKITE_BUILD_NUMBER%" --version="0.0.0.%BUILDKITE_BUILD_NUMBER%" --project="beheer" --server="%OCTO_HOST%" --apiKey %OCTO_APIKEY%