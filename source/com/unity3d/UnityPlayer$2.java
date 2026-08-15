package com.unity3d.player;

final class UnityPlayer$2 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$2(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        UnityPlayer.access$2100(this.a);
    }
}
