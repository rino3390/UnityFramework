# Unity Game Framework Package by Rino

原本是各自一個package，但後來決定整合成一個Framework package
```
https://github.com/rino3390/UnityFramework.git?path=GameFramework
```

### Package Manager設定
```
package.openupm.com
https://package.openupm.com

com.svermeulen.extenject
com.cysharp.messagepipe
com.cysharp.unitask
com.neuecc.unirx
```

包含

* DDD Core
* Utility
* GamaManager

## DDD Core
以Domain Driven Design為基礎的核心，目前包含Entity、Repository、EventSystem

未來可能會將現有的EventBus改成使用message pipe

## Utility
會記錄一些經常使用的遊戲方法，待擴充

## GameManager

使用Odin Inspector建構的遊戲管理器
