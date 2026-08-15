package com.unity3d.player;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.provider.Settings$System;

final class k {
    private Context a;
    private k$b b;

    public k(Context context) {
        this.a = context;
    }

    public final void a() {
        if (this.b != null) {
            this.a.getContentResolver().unregisterContentObserver(this.b);
            this.b = null;
        }
    }

    public final void a(k$a k_a, String str) {
        this.b = new k$b(this, new Handler(Looper.getMainLooper()), k_a);
        this.a.getContentResolver().registerContentObserver(Settings$System.getUriFor(str), true, this.b);
    }
}
