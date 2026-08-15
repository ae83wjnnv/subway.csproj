package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.os.Looper;
import com.google.android.play.core.assetpacks.AssetPackLocation;
import com.google.android.play.core.assetpacks.AssetPackManager;
import com.google.android.play.core.assetpacks.AssetPackManagerFactory;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;

final class a implements d {
    private static a a;
    private AssetPackManager b;
    private HashSet c;
    private Object d;

    private a(Context context) {
        if (a != null) {
            throw new RuntimeException("AssetPackManagerWrapper should be created only once. Use getInstance() instead.");
        }
        this.b = AssetPackManagerFactory.getInstance(context);
        this.c = new HashSet();
    }

    static a a() {
        return a;
    }

    public static d a(Context context) {
        if (a == null) {
            a = new a(context);
        }
        return a;
    }

    static HashSet a(a aVar) {
        return aVar.c;
    }

    static void a(a aVar, String str, IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback, Looper looper) {
        aVar.a(str, iAssetPackManagerDownloadStatusCallback, looper);
    }

    private void a(String str, IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback, Looper looper) {
        synchronized (a) {
            if (this.d == null) {
                a$b a_b = new a$b(this, iAssetPackManagerDownloadStatusCallback, looper);
                this.b.registerListener(a_b);
                this.d = a_b;
            } else {
                ((a$b) this.d).a(iAssetPackManagerDownloadStatusCallback);
            }
            this.c.add(str);
            this.b.fetch(Collections.singletonList(str));
        }
    }

    static Object b(a aVar) {
        return aVar.d;
    }

    static Object c(a aVar) {
        aVar.d = null;
        return null;
    }

    @Override
    public final Object a(IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback) {
        a$b a_b = new a$b(this, iAssetPackManagerDownloadStatusCallback);
        this.b.registerListener(a_b);
        return a_b;
    }

    @Override
    public final String a(String str) {
        AssetPackLocation packLocation = this.b.getPackLocation(str);
        return packLocation == null ? "" : packLocation.assetsPath();
    }

    @Override
    public final void a(Activity activity, IAssetPackManagerMobileDataConfirmationCallback iAssetPackManagerMobileDataConfirmationCallback) {
        this.b.showCellularDataConfirmation(activity).addOnSuccessListener(new a$c(iAssetPackManagerMobileDataConfirmationCallback));
    }

    @Override
    public final void a(Object obj) {
        if (obj instanceof a$b) {
            this.b.unregisterListener((a$b) obj);
        }
    }

    @Override
    public final void a(String[] strArr) {
        this.b.cancel(Arrays.asList(strArr));
    }

    @Override
    public final void a(String[] strArr, IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback) {
        for (String str : strArr) {
            this.b.getPackStates(Collections.singletonList(str)).addOnCompleteListener(new a$d(iAssetPackManagerDownloadStatusCallback, str));
        }
    }

    @Override
    public final void a(String[] strArr, IAssetPackManagerStatusQueryCallback iAssetPackManagerStatusQueryCallback) {
        this.b.getPackStates(Arrays.asList(strArr)).addOnCompleteListener(new a$e(iAssetPackManagerStatusQueryCallback, strArr));
    }

    @Override
    public final void b(String str) {
        this.b.removePack(str);
    }
}
