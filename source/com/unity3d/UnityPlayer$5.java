package com.unity3d.player;

import android.graphics.Rect;

final class UnityPlayer$5 implements Runnable {
    final UnityPlayer a;

    UnityPlayer$5(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void run() {
        this.a.reportSoftInputArea(new Rect());
        this.a.reportSoftInputIsVisible(false);
        if (this.a.mSoftInputDialog != null) {
            this.a.mSoftInputDialog.dismiss();
            this.a.mSoftInputDialog = null;
            UnityPlayer.access$2500(this.a);
        }
    }
}
