package com.unity3d.player;

import android.os.Build$VERSION;

class g$a implements Runnable {
    final g a;
    private IPermissionRequestCallbacks b;
    private String c;
    private int d;
    private boolean e;

    g$a(g gVar, IPermissionRequestCallbacks iPermissionRequestCallbacks, String str, int i, boolean z) {
        this.a = gVar;
        this.b = iPermissionRequestCallbacks;
        this.c = str;
        this.d = i;
        this.e = z;
    }

    @Override
    public final void run() {
        int i = this.d;
        if (i != -1) {
            if (i == 0) {
                this.b.onPermissionGranted(this.c);
            }
        } else if (Build$VERSION.SDK_INT >= 30 || this.e) {
            this.b.onPermissionDenied(this.c);
        } else {
            this.b.onPermissionDeniedAndDontAskAgain(this.c);
        }
    }
}
