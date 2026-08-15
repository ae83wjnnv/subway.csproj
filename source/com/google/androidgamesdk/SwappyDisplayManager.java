package com.google.androidgamesdk;

import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.hardware.display.DisplayManager;
import android.hardware.display.DisplayManager$DisplayListener;
import android.os.Build$VERSION;
import android.util.Log;
import android.view.Display;
import android.view.Display$Mode;
import android.view.WindowManager;

public class SwappyDisplayManager implements DisplayManager$DisplayListener {
    private Activity mActivity;
    private long mCookie;
    private Display$Mode mCurrentMode;
    private SwappyDisplayManager$a mLooper;
    private WindowManager mWindowManager;
    private final String LOG_TAG = "SwappyDisplayManager";
    private final boolean DEBUG = false;
    private final long ONE_MS_IN_NS = 1000000;
    private final long ONE_S_IN_NS = 1000000000;

    public SwappyDisplayManager(long j, Activity activity) {
        String string;
        try {
            ActivityInfo activityInfo = activity.getPackageManager().getActivityInfo(activity.getIntent().getComponent(), 128);
            if (activityInfo.metaData != null && (string = activityInfo.metaData.getString("android.app.lib_name")) != null) {
                System.loadLibrary(string);
            }
        } catch (Throwable th) {
            Log.e("SwappyDisplayManager", th.getMessage());
        }
        this.mCookie = j;
        this.mActivity = activity;
        WindowManager windowManager = (WindowManager) activity.getSystemService(WindowManager.class);
        this.mWindowManager = windowManager;
        Display defaultDisplay = windowManager.getDefaultDisplay();
        this.mCurrentMode = defaultDisplay.getMode();
        updateSupportedRefreshRates(defaultDisplay);
        DisplayManager displayManager = (DisplayManager) this.mActivity.getSystemService(DisplayManager.class);
        synchronized (this) {
            SwappyDisplayManager$a swappyDisplayManager$a = new SwappyDisplayManager$a(this, (byte) 0);
            this.mLooper = swappyDisplayManager$a;
            swappyDisplayManager$a.start();
            displayManager.registerDisplayListener(this, this.mLooper.a);
        }
    }

    static Activity access$100(SwappyDisplayManager swappyDisplayManager) {
        return swappyDisplayManager.mActivity;
    }

    private boolean callNativeCallback() {
        if (Build$VERSION.SDK_INT >= 29) {
            return Build$VERSION.SDK_INT == 29 && Build$VERSION.PREVIEW_SDK_INT == 0;
        }
        return true;
    }

    private boolean modeMatchesCurrentResolution(Display$Mode display$Mode) {
        return display$Mode.getPhysicalHeight() == this.mCurrentMode.getPhysicalHeight() && display$Mode.getPhysicalWidth() == this.mCurrentMode.getPhysicalWidth();
    }

    private native void nOnRefreshRateChanged(long j, long j2, long j3, long j4);

    private native void nSetSupportedRefreshRates(long j, long[] jArr, int[] iArr);

    private void updateSupportedRefreshRates(Display display) {
        Display$Mode[] supportedModes = display.getSupportedModes();
        int i = 0;
        for (Display$Mode display$Mode : supportedModes) {
            if (modeMatchesCurrentResolution(display$Mode)) {
                i++;
            }
        }
        long[] jArr = new long[i];
        int[] iArr = new int[i];
        int i2 = 0;
        for (int i3 = 0; i3 < supportedModes.length; i3++) {
            if (modeMatchesCurrentResolution(supportedModes[i3])) {
                jArr[i2] = (long) (1.0E9f / supportedModes[i3].getRefreshRate());
                iArr[i2] = supportedModes[i3].getModeId();
                i2++;
            }
        }
        nSetSupportedRefreshRates(this.mCookie, jArr, iArr);
    }

    @Override
    public void onDisplayAdded(int i) {
    }

    @Override
    public void onDisplayChanged(int i) {
        synchronized (this) {
            Display defaultDisplay = this.mWindowManager.getDefaultDisplay();
            float refreshRate = defaultDisplay.getRefreshRate();
            Display$Mode mode = defaultDisplay.getMode();
            boolean z = true;
            boolean z2 = (mode.getPhysicalWidth() != this.mCurrentMode.getPhysicalWidth()) | (mode.getPhysicalHeight() != this.mCurrentMode.getPhysicalHeight());
            if (refreshRate == this.mCurrentMode.getRefreshRate()) {
                z = false;
            }
            this.mCurrentMode = mode;
            if (z2) {
                updateSupportedRefreshRates(defaultDisplay);
            }
            if (callNativeCallback() && z) {
                long j = (long) (1.0E9f / refreshRate);
                nOnRefreshRateChanged(this.mCookie, j, defaultDisplay.getAppVsyncOffsetNanos(), j - (this.mWindowManager.getDefaultDisplay().getPresentationDeadlineNanos() - 1000000));
            }
        }
    }

    @Override
    public void onDisplayRemoved(int i) {
    }

    public void setPreferredRefreshRate(int i) {
        this.mActivity.runOnUiThread(new SwappyDisplayManager$1(this, i));
    }

    public void terminate() {
        this.mLooper.a.getLooper().quit();
    }
}
