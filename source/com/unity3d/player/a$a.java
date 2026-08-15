package com.unity3d.player;

import java.util.Iterator;
import java.util.Set;

class a$a implements Runnable {
    private Set a;
    private String b;
    private int c;
    private long d;
    private long e;
    private int f;
    private int g;

    a$a(Set set, String str, int i, long j, long j2, int i2, int i3) {
        this.a = set;
        this.b = str;
        this.c = i;
        this.d = j;
        this.e = j2;
        this.f = i2;
        this.g = i3;
    }

    @Override
    public final void run() {
        Iterator it = this.a.iterator();
        while (it.hasNext()) {
            ((IAssetPackManagerDownloadStatusCallback) it.next()).onStatusUpdate(this.b, this.c, this.d, this.e, this.f, this.g);
        }
    }
}
