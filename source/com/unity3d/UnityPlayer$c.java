package com.unity3d.player;

import android.telephony.PhoneStateListener;

class UnityPlayer$c extends PhoneStateListener {
    final UnityPlayer a;

    private UnityPlayer$c(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    UnityPlayer$c(UnityPlayer unityPlayer, byte b) {
        this(unityPlayer);
    }

    @Override
    public final void onCallStateChanged(int i, String str) {
        UnityPlayer.access$800(this.a, i == 1);
    }
}
