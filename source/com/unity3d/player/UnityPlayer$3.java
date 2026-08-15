package com.unity3d.player;

final class UnityPlayer$3 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$3(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        UnityPlayer.access$2200(this.a);
        this.a.runOnUiThread(new UnityPlayer$3$1(this));
    }
}
