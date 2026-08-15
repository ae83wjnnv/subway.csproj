package com.unity3d.player;

final class UnityPlayer$8 implements Runnable {
    final boolean a;
    final UnityPlayer b;

    UnityPlayer$8(UnityPlayer unityPlayer, boolean z) {
        this.b = unityPlayer;
        this.a = z;
    }

    @Override
    public final void run() {
        if (this.b.mSoftInputDialog != null) {
            this.b.mSoftInputDialog.a(this.a);
        }
    }
}
