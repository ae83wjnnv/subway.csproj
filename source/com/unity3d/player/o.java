package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import java.util.concurrent.Semaphore;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

final class o {
    private UnityPlayer a;
    private o$a c;
    private Context b = null;
    private final Semaphore d = new Semaphore(0);
    private final Lock e = new ReentrantLock();
    private n f = null;
    private int g = 2;
    private boolean h = false;
    private boolean i = false;

    o(UnityPlayer unityPlayer) {
        this.a = null;
        this.a = unityPlayer;
    }

    static int a(o oVar, int i) {
        oVar.g = i;
        return i;
    }

    static n a(o oVar) {
        return oVar.f;
    }

    static n a(o oVar, n nVar) {
        oVar.f = nVar;
        return nVar;
    }

    static Semaphore b(o oVar) {
        return oVar.d;
    }

    static Context c(o oVar) {
        return oVar.b;
    }

    static Lock d(o oVar) {
        return oVar.e;
    }

    private void d() {
        n nVar = this.f;
        if (nVar != null) {
            this.a.removeViewFromPlayer(nVar);
            this.i = false;
            this.f.destroyPlayer();
            this.f = null;
            o$a o_a = this.c;
            if (o_a != null) {
                o_a.a();
            }
        }
    }

    static boolean e(o oVar) {
        return oVar.i;
    }

    static void f(o oVar) {
        oVar.d();
    }

    static UnityPlayer g(o oVar) {
        return oVar.a;
    }

    static boolean h(o oVar) {
        oVar.i = true;
        return true;
    }

    public final void a() {
        this.e.lock();
        n nVar = this.f;
        if (nVar != null) {
            if (this.g == 0) {
                nVar.CancelOnPrepare();
            } else if (this.i) {
                boolean zA = nVar.a();
                this.h = zA;
                if (!zA) {
                    this.f.pause();
                }
            }
        }
        this.e.unlock();
    }

    public final boolean a(Context context, String str, int i, int i2, int i3, boolean z, long j, long j2, o$a o_a) {
        this.e.lock();
        this.c = o_a;
        this.b = context;
        this.d.drainPermits();
        this.g = 2;
        runOnUiThread(new o$1(this, str, i, i2, i3, z, j, j2));
        boolean z2 = false;
        try {
            this.e.unlock();
            this.d.acquire();
            this.e.lock();
            if (this.g != 2) {
                z2 = true;
            }
        } catch (InterruptedException unused) {
        }
        runOnUiThread(new o$2(this));
        runOnUiThread((!z2 || this.g == 3) ? new o$4(this) : new o$3(this));
        this.e.unlock();
        return z2;
    }

    public final void b() {
        this.e.lock();
        n nVar = this.f;
        if (nVar != null && this.i && !this.h) {
            nVar.start();
        }
        this.e.unlock();
    }

    public final void c() {
        this.e.lock();
        n nVar = this.f;
        if (nVar != null) {
            nVar.updateVideoLayout();
        }
        this.e.unlock();
    }

    protected final void runOnUiThread(Runnable runnable) {
        Context context = this.b;
        if (context instanceof Activity) {
            ((Activity) context).runOnUiThread(runnable);
        } else {
            f.Log(5, "Not running from an Activity; Ignoring execution request...");
        }
    }
}
