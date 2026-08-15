package com.unity3d.player;

final class UnityPlayer$22 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$22(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        if (UnityPlayer.access$1700(this.a)) {
            UnityPlayer unityPlayer = this.a;
            unityPlayer.removeView(UnityPlayer.access$1400(unityPlayer));
        } else {
            UnityPlayer unityPlayer2 = this.a;
            unityPlayer2.addView(UnityPlayer.access$1400(unityPlayer2));
        }
    }
}
