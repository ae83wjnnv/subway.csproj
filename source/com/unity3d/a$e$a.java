package com.unity3d.player;

class a$e$a implements Runnable {
    private IAssetPackManagerStatusQueryCallback a;
    private long b;
    private String[] c;
    private int[] d;
    private int[] e;

    a$e$a(IAssetPackManagerStatusQueryCallback iAssetPackManagerStatusQueryCallback, long j, String[] strArr, int[] iArr, int[] iArr2) {
        this.a = iAssetPackManagerStatusQueryCallback;
        this.b = j;
        this.c = strArr;
        this.d = iArr;
        this.e = iArr2;
    }

    @Override
    public final void run() {
        this.a.onStatusResult(this.b, this.c, this.d, this.e);
    }
}
