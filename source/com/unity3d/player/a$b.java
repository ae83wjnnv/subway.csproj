package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import com.google.android.play.core.assetpacks.AssetPackState;
import com.google.android.play.core.assetpacks.AssetPackStateUpdateListener;
import java.util.HashSet;
import java.util.Set;

class a$b implements AssetPackStateUpdateListener {
    final a a;
    private HashSet b;
    private Looper c;

    public a$b(a aVar, IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback) {
        this(aVar, iAssetPackManagerDownloadStatusCallback, Looper.myLooper());
    }

    public a$b(a aVar, IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback, Looper looper) {
        this.a = aVar;
        HashSet hashSet = new HashSet();
        this.b = hashSet;
        hashSet.add(iAssetPackManagerDownloadStatusCallback);
        this.c = looper;
    }

    private static Set a(HashSet hashSet) {
        return (Set) hashSet.clone();
    }

    private synchronized void a(AssetPackState assetPackState) {
        if (assetPackState.status() == 4 || assetPackState.status() == 5 || assetPackState.status() == 0) {
            synchronized (a.a()) {
                a.a(this.a).remove(assetPackState.name());
                if (a.a(this.a).isEmpty()) {
                    this.a.a(a.b(this.a));
                    a.c(this.a);
                }
            }
        }
        if (this.b.size() == 0) {
            return;
        }
        new Handler(this.c).post(new a$a(a(this.b), assetPackState.name(), assetPackState.status(), assetPackState.totalBytesToDownload(), assetPackState.bytesDownloaded(), assetPackState.transferProgressPercentage(), assetPackState.errorCode()));
    }

    public final synchronized void a(IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback) {
        this.b.add(iAssetPackManagerDownloadStatusCallback);
    }

    public final void onStateUpdate(Object obj) {
        a((AssetPackState) obj);
    }
}
