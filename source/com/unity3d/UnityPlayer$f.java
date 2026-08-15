package com.unity3d.player;

abstract class UnityPlayer$f implements Runnable {
    final UnityPlayer e;

    private UnityPlayer$f(UnityPlayer unityPlayer) {
        this.e = unityPlayer;
    }

    UnityPlayer$f(UnityPlayer unityPlayer, byte b) {
        this(unityPlayer);
    }

    public abstract void a();

    @Override
    public final void run() {
        if (this.e.isFinishing()) {
            return;
        }
        a();
    }
}
