#!/bin/sh

package_filename=${JOB_NAME}_${version}_${SVN_REVISION}_${BUILD_NUMBER}

echo "package_filename="$package_filename

#UNITY程序的路径#
UNITY_PATH=/Applications/Unity/Unity.app/Contents/MacOS/Unity

#游戏程序路径#
PROJECT_PATH=${WORKSPACE}
#IOS打包脚本路径#
BUILD_IOS_PATH=${PROJECT_PATH}/CommandLines/buildios.sh

echo ${USER_PWD} | sudo -S chmod +x $BUILD_IOS_PATH
echo ${USER_PWD} | sudo -S chmod +x ${PROJECT_PATH}/CommandLines/cleanafterbuild.sh



#switch buildtarget
$UNITY_PATH -quit -batchmode -projectPath $PROJECT_PATH -logFile /tmp/1.log  -executeMethod Publish.checkBuildTarget "$version" "$serverId" "$sdk" "$buildTarget" "$isDevMode" "$isBuildAB" "$bundleId" $package_filename

echo "switched buildtarget"

$UNITY_PATH -quit -batchmode -projectPath $PROJECT_PATH -logFile /tmp/1.log  -executeMethod Publish.commandLineBuild "$version" "$serverId" "$sdk" "$buildTarget" "$isDevMode" "$isBuildAB" "$bundleId"  $package_filename

echo "unity3d builded"

if [ $buildTarget = "android" ];then
exit
fi

if [ $buildTarget = "ios" ];then

app_name=${bundleId##*.}

security unlock-keychain -p ${USER_PWD} /Users/${USER_NAME}/Library/Keychains/login.keychain

#开始生成ipa

$BUILD_IOS_PATH $PROJECT_PATH/xcodeprj $app_name $package_filename

echo "ipa生成完毕"
fi




