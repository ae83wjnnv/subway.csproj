package com.unity3d.player;

final class UnityPlayer$20 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$20(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        UnityPlayer.access$1500(this.a);
    }
}
