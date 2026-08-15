package com.unity3d.player;

final class UnityPlayer$9 implements Runnable {
    final int a;
    final int b;
    final UnityPlayer c;

    UnityPlayer$9(UnityPlayer unityPlayer, int i, int i2) {
        this.c = unityPlayer;
        this.a = i;
        this.b = i2;
    }

    @Override
    public final void run() {
        if (this.c.mSoftInputDialog != null) {
            this.c.mSoftInputDialog.a(this.a, this.b);
        }
    }
}
