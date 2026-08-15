package com.unity3d.player;

import android.app.Activity;

final class UnityPlayer$15 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$15(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        if (!UnityPlayer.access$3400(this.a) || UnityPlayer.access$3500(this.a) == null) {
            return;
        }
        ((Activity) UnityPlayer.access$2300(this.a)).setRequestedOrientation(UnityPlayer.access$3600(this.a));
    }
}
