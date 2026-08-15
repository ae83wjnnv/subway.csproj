package com.unity3d.player;

import android.content.DialogInterface;
import android.content.DialogInterface$OnClickListener;

final class UnityPlayer$1 implements DialogInterface$OnClickListener {
    final UnityPlayer a;

    UnityPlayer$1(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void onClick(DialogInterface dialogInterface, int i) {
        UnityPlayer.access$400(this.a);
    }
}
