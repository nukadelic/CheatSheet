## Remove system apps without root access

list all packages with the keyword 'facebook'
> `adb shell pm list packages -f | grep facebook`

view package premissions
> `adb shell dumpsys package packagename`

root uninstall
> `adb shell pm uninstall --user 0 jp.co.disney.apps.base.disneymarketapp`
