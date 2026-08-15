package com.unity3d.player;

final class UnityPlayer$6 implements Runnable {
    final String a;
    final UnityPlayer b;

    UnityPlayer$6(UnityPlayer unityPlayer, String str) {
        this.b = unityPlayer;
        this.a = str;
    }

    @Override
    public final void run() {
        if (this.b.mSoftInputDialog == null || this.a == null) {
            return;
        }
        this.b.mSoftInputDialog.a(this.a);
    }
}
