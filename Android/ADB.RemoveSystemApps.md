# Android ADB

## Remove / enable / disable / download system apps without root 

Search for packages with a keyword `faceb`
>`adb shell pm list packages | grep faceb`

View package premissions
> `adb shell dumpsys package <package_name>`

Disable any package ( including system )
>`adb shell pm disable-user --user 0 <package_name>`

Enable any package 
>`adb shell pm enable <package_name>`

Uninstall any package ( including system ) , `-k` is optional 
>`adb shell pm uninstall -k --user 0 <package_name>`

List disabled packages
>`adb shell pm list packages -d`

List enabled packages
>`adb shell pm list packages -e`

List only system packages
>`adb shell pm list packages -s`

List none system packages (3rd party)
>`adb shell pm list packages -3`

Show full path + apk 
>`adb shell pm list packages -f`

Download apk to desktop
>`adb pull path/to/app.apk path/to/dekstop/app.apk`
