package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;

class UnityPlayer$e extends Thread {
    Handler a;
    boolean b;
    boolean c;
    int d;
    int e;
    int f;
    int g;
    int h;
    final UnityPlayer i;

    private UnityPlayer$e(UnityPlayer unityPlayer) {
        this.i = unityPlayer;
        this.b = false;
        this.c = false;
        this.d = UnityPlayer$b.b;
        this.e = 0;
        this.h = 5;
    }

    UnityPlayer$e(UnityPlayer unityPlayer, byte b) {
        this(unityPlayer);
    }

    private void a(UnityPlayer$d unityPlayer$d) {
        Handler handler = this.a;
        if (handler != null) {
            Message.obtain(handler, 2269, unityPlayer$d).sendToTarget();
        }
    }

    public final void a() {
        a(UnityPlayer$d.QUIT);
    }

    public final void a(int i, int i2) {
        this.f = i;
        this.g = i2;
        a(UnityPlayer$d.ORIENTATION_ANGLE_CHANGE);
    }

    public final void a(Runnable runnable) {
        if (this.a == null) {
            return;
        }
        a(UnityPlayer$d.PAUSE);
        Message.obtain(this.a, runnable).sendToTarget();
    }

    public final void b() {
        a(UnityPlayer$d.RESUME);
    }

    public final void b(Runnable runnable) {
        if (this.a == null) {
            return;
        }
        a(UnityPlayer$d.SURFACE_LOST);
        Message.obtain(this.a, runnable).sendToTarget();
    }

    public final void c() {
        a(UnityPlayer$d.FOCUS_GAINED);
    }

    public final void c(Runnable runnable) {
        Handler handler = this.a;
        if (handler == null) {
            return;
        }
        Message.obtain(handler, runnable).sendToTarget();
        a(UnityPlayer$d.SURFACE_ACQUIRED);
    }

    public final void d() {
        a(UnityPlayer$d.FOCUS_LOST);
    }

    public final void d(Runnable runnable) {
        Handler handler = this.a;
        if (handler != null) {
            Message.obtain(handler, runnable).sendToTarget();
        }
    }

    public final void e() {
        a(UnityPlayer$d.URL_ACTIVATED);
    }

    @Override
    public final void run() {
        setName("UnityMain");
        Looper.prepare();
        this.a = new Handler(new UnityPlayer$e$1(this));
        Looper.loop();
    }
}
