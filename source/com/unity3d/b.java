package com.unity3d.player;

import android.content.Context;
import android.media.AudioManager;
import android.os.Handler;
import android.provider.Settings$System;

final class b {
    private final Context a;
    private final AudioManager b;
    private b$a c;

    public b(Context context) {
        this.a = context;
        this.b = (AudioManager) context.getSystemService("audio");
    }

    public final void a() {
        if (this.c != null) {
            this.a.getContentResolver().unregisterContentObserver(this.c);
            this.c = null;
        }
    }

    public final void a(b$b b_b) {
        this.c = new b$a(this, new Handler(), this.b, 3, b_b);
        this.a.getContentResolver().registerContentObserver(Settings$System.CONTENT_URI, true, this.c);
    }
}
