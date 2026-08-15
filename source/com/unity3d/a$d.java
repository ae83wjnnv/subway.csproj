package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import com.google.android.play.core.assetpacks.AssetPackState;
import com.google.android.play.core.assetpacks.AssetPackStates;
import com.google.android.play.core.tasks.OnCompleteListener;
import com.google.android.play.core.tasks.RuntimeExecutionException;
import com.google.android.play.core.tasks.Task;
import java.util.Collections;
import java.util.Map;

class a$d implements OnCompleteListener {
    private IAssetPackManagerDownloadStatusCallback a;
    private Looper b = Looper.myLooper();
    private String c;

    public a$d(IAssetPackManagerDownloadStatusCallback iAssetPackManagerDownloadStatusCallback, String str) {
        this.a = iAssetPackManagerDownloadStatusCallback;
        this.c = str;
    }

    private void a(String str, int i, int i2, long j) {
        new Handler(this.b).post(new a$a(Collections.singleton(this.a), str, i, j, i == 4 ? j : 0L, 0, i2));
    }

    public final void onComplete(Task task) {
        try {
            AssetPackStates assetPackStates = (AssetPackStates) task.getResult();
            Map mapPackStates = assetPackStates.packStates();
            if (mapPackStates.size() == 0) {
                return;
            }
            for (AssetPackState assetPackState : mapPackStates.values()) {
                if (assetPackState.errorCode() != 0 || assetPackState.status() == 4 || assetPackState.status() == 5 || assetPackState.status() == 0) {
                    a(assetPackState.name(), assetPackState.status(), assetPackState.errorCode(), assetPackStates.totalBytes());
                } else {
                    a.a(a.a(), assetPackState.name(), this.a, this.b);
                }
            }
        } catch (RuntimeExecutionException e) {
            a(this.c, 0, e.getErrorCode(), 0L);
        }
    }
}
