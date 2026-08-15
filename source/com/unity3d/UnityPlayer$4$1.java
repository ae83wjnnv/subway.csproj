package com.unity3d.player;

import android.content.DialogInterface;
import android.content.DialogInterface$OnCancelListener;

final class UnityPlayer$4$1 implements DialogInterface$OnCancelListener {
    final UnityPlayer$4 a;

    UnityPlayer$4$1(UnityPlayer$4 unityPlayer$4) {
        this.a = unityPlayer$4;
    }

    @Override
    public final void onCancel(DialogInterface dialogInterface) {
        UnityPlayer.access$2400(this.a.l);
        this.a.l.reportSoftInputStr(null, 1, false);
    }
}
