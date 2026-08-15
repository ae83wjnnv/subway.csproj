package com.unity3d.player;

final class UnityPlayer$7 implements Runnable {
    final int a;
    final UnityPlayer b;

    UnityPlayer$7(UnityPlayer unityPlayer, int i) {
        this.b = unityPlayer;
        this.a = i;
    }

    @Override
    public final void run() {
        if (this.b.mSoftInputDialog != null) {
            this.b.mSoftInputDialog.a(this.a);
        }
    }
}
