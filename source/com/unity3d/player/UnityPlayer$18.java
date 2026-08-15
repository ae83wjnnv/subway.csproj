package com.unity3d.player;

final class UnityPlayer$18 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$18(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        UnityPlayer unityPlayer = this.a;
        unityPlayer.removeView(UnityPlayer.access$1000(unityPlayer));
        UnityPlayer.access$1002(this.a, null);
    }
}
