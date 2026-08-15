package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import com.google.android.play.core.assetpacks.AssetPackState;
import com.google.android.play.core.assetpacks.AssetPackStates;
import com.google.android.play.core.tasks.OnCompleteListener;
import com.google.android.play.core.tasks.RuntimeExecutionException;
import com.google.android.play.core.tasks.Task;
import java.util.Map;

class a$e implements OnCompleteListener {
    private IAssetPackManagerStatusQueryCallback a;
    private Looper b = Looper.myLooper();
    private String[] c;

    public a$e(IAssetPackManagerStatusQueryCallback iAssetPackManagerStatusQueryCallback, String[] strArr) {
        this.a = iAssetPackManagerStatusQueryCallback;
        this.c = strArr;
    }

    public final void onComplete(Task task) {
        if (this.a == null) {
            return;
        }
        int i = 0;
        try {
            AssetPackStates assetPackStates = (AssetPackStates) task.getResult();
            Map mapPackStates = assetPackStates.packStates();
            int size = mapPackStates.size();
            String[] strArr = new String[size];
            int[] iArr = new int[size];
            int[] iArr2 = new int[size];
            for (AssetPackState assetPackState : mapPackStates.values()) {
                strArr[i] = assetPackState.name();
                iArr[i] = assetPackState.status();
                iArr2[i] = assetPackState.errorCode();
                i++;
            }
            new Handler(this.b).post(new a$e$a(this.a, assetPackStates.totalBytes(), strArr, iArr, iArr2));
        } catch (RuntimeExecutionException e) {
            String message = e.getMessage();
            for (String str : this.c) {
                if (message.contains(str)) {
                    new Handler(this.b).post(new a$e$a(this.a, 0L, new String[]{str}, new int[]{0}, new int[]{e.getErrorCode()}));
                    return;
                }
            }
            String[] strArr2 = this.c;
            int[] iArr3 = new int[strArr2.length];
            int[] iArr4 = new int[strArr2.length];
            for (int i2 = 0; i2 < this.c.length; i2++) {
                iArr3[i2] = 0;
                iArr4[i2] = e.getErrorCode();
            }
            new Handler(this.b).post(new a$e$a(this.a, 0L, this.c, iArr3, iArr4));
        }
    }
}
