package com.unity3d.player;

final class UnityPlayer$16 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$16(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        this.a.pause();
        this.a.windowFocusChanged(false);
        UnityPlayer.access$3700(this.a).onUnityPlayerUnloaded();
    }
}
