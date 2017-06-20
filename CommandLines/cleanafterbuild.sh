# #!/bin/bash  
package_filename=${JOB_NAME}_${version}_${SVN_REVISION}_${BUILD_NUMBER}

#游戏程序路径#
PROJECT_PATH=${WORKSPACE}
echo "buildTarget="$buildTarget
if [ $buildTarget = "android" ];then
rm ${PROJECT_PATH}/${package_filename}.apk
fi

if [ $buildTarget = "ios" ];then
rm ${PROJECT_PATH}/${package_filename}.ipa
fi