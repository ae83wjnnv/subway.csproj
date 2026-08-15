package com.unity3d.player;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager$RunningAppProcessInfo;
import android.app.AlertDialog;
import android.app.AlertDialog$Builder;
import android.content.BroadcastReceiver;
import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.Configuration;
import android.content.res.TypedArray;
import android.graphics.Rect;
import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Process;
import android.telephony.TelephonyManager;
import android.view.InputEvent;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.OrientationEventListener;
import android.view.Surface;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.view.WindowManager;
import android.widget.FrameLayout;
import java.io.UnsupportedEncodingException;
import java.util.List;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;

public class UnityPlayer extends FrameLayout implements IUnityPlayerLifecycleEvents {
    private static final int ANR_TIMEOUT_SECONDS = 4;
    private static final String ARCORE_ENABLE_METADATA_NAME = "unity.arcore-enable";
    private static final String LAUNCH_FULLSCREEN = "unity.launch-fullscreen";
    private static final int RUN_STATE_CHANGED_MSG_CODE = 2269;
    private static final String SPLASH_ENABLE_METADATA_NAME = "unity.splash-enable";
    private static final String SPLASH_MODE_METADATA_NAME = "unity.splash-mode";
    public static Activity currentActivity;
    public static String m_AndroidFilesDir;
    private static String m_InstantGameEngine;
    private static String m_InstantGameName;
    private Activity mActivity;
    private Context mContext;
    private SurfaceView mGlView;
    Handler mHandler;
    private int mInitialScreenOrientation;
    private boolean mIsFullscreen;
    private BroadcastReceiver mKillingIsMyBusiness;
    private boolean mMainDisplayOverride;
    private int mNaturalOrientation;
    private OrientationEventListener mOrientationListener;
    private boolean mProcessKillRequested;
    private boolean mQuitting;
    i mSoftInputDialog;
    private m mState;
    private o mVideoPlayerProxy;
    private GoogleARCoreApi m_ARCoreApi;
    private boolean m_AddPhoneCallListener;
    private AudioVolumeHandler m_AudioVolumeHandler;
    private Camera2Wrapper m_Camera2Wrapper;
    private ClipboardManager m_ClipboardManager;
    private final ConcurrentLinkedQueue m_Events;
    private UnityPlayer$a m_FakeListener;
    private HFPStatus m_HFPStatus;
    UnityPlayer$e m_MainThread;
    private NetworkConnectivity m_NetworkConnectivity;
    private OrientationLockListener m_OrientationLockListener;
    private h m_PersistentUnitySurface;
    private UnityPlayer$c m_PhoneCallListener;
    private j m_SplashScreen;
    private TelephonyManager m_TelephonyManager;
    private IUnityPlayerLifecycleEvents m_UnityPlayerLifecycleEvents;
    private Uri m_launchUri;

    static {
        new l().a();
    }

    public UnityPlayer(Context context) {
        this(context, null);
    }

    public UnityPlayer(Context context, IUnityPlayerLifecycleEvents iUnityPlayerLifecycleEvents) {
        super(context);
        this.mHandler = new Handler();
        this.mInitialScreenOrientation = -1;
        this.mMainDisplayOverride = false;
        this.mIsFullscreen = true;
        this.mState = new m();
        this.m_Events = new ConcurrentLinkedQueue();
        this.mKillingIsMyBusiness = null;
        this.mOrientationListener = null;
        this.m_MainThread = new UnityPlayer$e(this, (byte) 0);
        this.m_AddPhoneCallListener = false;
        this.m_PhoneCallListener = new UnityPlayer$c(this, (byte) 0);
        this.m_ARCoreApi = null;
        this.m_FakeListener = new UnityPlayer$a(this);
        this.m_Camera2Wrapper = null;
        this.m_HFPStatus = null;
        this.m_AudioVolumeHandler = null;
        this.m_OrientationLockListener = null;
        this.m_launchUri = null;
        this.m_NetworkConnectivity = null;
        this.m_UnityPlayerLifecycleEvents = null;
        this.mProcessKillRequested = true;
        this.mSoftInputDialog = null;
        this.m_UnityPlayerLifecycleEvents = iUnityPlayerLifecycleEvents == null ? this : iUnityPlayerLifecycleEvents;
        if (context instanceof Activity) {
            Activity activity = (Activity) context;
            this.mActivity = activity;
            currentActivity = activity;
            this.mInitialScreenOrientation = activity.getRequestedOrientation();
            this.m_launchUri = this.mActivity.getIntent().getData();
            m_InstantGameName = currentActivity.getIntent().getStringExtra("instantGame");
            String stringExtra = currentActivity.getIntent().getStringExtra("engineFolder");
            m_InstantGameEngine = stringExtra;
            if (stringExtra == null) {
                m_InstantGameEngine = "2019";
            }
            m_AndroidFilesDir = context.getFilesDir().getAbsolutePath();
            if (m_InstantGameName != null) {
                String stringExtra2 = currentActivity.getIntent().getStringExtra("unity");
                currentActivity.getIntent().putExtra("unity", (((stringExtra2 == null ? "" : stringExtra2) + " -instantGame " + m_InstantGameName) + " -instantGameEngine " + m_InstantGameEngine) + " -overrideMonoSearchPath " + ((m_AndroidFilesDir + "/UnityPlayers/" + m_InstantGameEngine) + "/Managed"));
            }
        }
        this.mContext = context;
        EarlyEnableFullScreenIfEnabled(currentActivity);
        this.mNaturalOrientation = getNaturalOrientation(getResources().getConfiguration().orientation);
        if (this.mActivity != null && getSplashEnabled()) {
            j jVar = new j(this.mContext, j$a.a()[getSplashMode()]);
            this.m_SplashScreen = jVar;
            addView(jVar);
        }
        if (currentActivity != null) {
            this.m_PersistentUnitySurface = new h(this.mContext);
        }
        String strLoadNative = loadNative(getUnityNativeLibraryPath(this.mContext));
        if (!m.c()) {
            f.Log(6, "Your hardware does not support this application.");
            AlertDialog alertDialogCreate = new AlertDialog$Builder(this.mContext).setTitle("Failure to initialize!").setPositiveButton("OK", new UnityPlayer$1(this)).setMessage("Your hardware does not support this application.\n\n" + strLoadNative + "\n\n Press OK to quit.").create();
            alertDialogCreate.setCancelable(false);
            alertDialogCreate.show();
            return;
        }
        initJni(context);
        this.mState.c(true);
        SurfaceView surfaceViewCreateGlView = CreateGlView();
        this.mGlView = surfaceViewCreateGlView;
        surfaceViewCreateGlView.setContentDescription(GetGlViewContentDescription(context));
        addView(this.mGlView);
        bringChildToFront(this.m_SplashScreen);
        this.mQuitting = false;
        hideStatusBar();
        this.m_TelephonyManager = (TelephonyManager) this.mContext.getSystemService("phone");
        this.m_ClipboardManager = (ClipboardManager) this.mContext.getSystemService("clipboard");
        this.m_Camera2Wrapper = new Camera2Wrapper(this.mContext);
        this.m_HFPStatus = new HFPStatus(this.mContext);
        this.m_MainThread.start();
    }

    private SurfaceView CreateGlView() {
        SurfaceView surfaceView = new SurfaceView(this.mContext);
        surfaceView.setId(this.mContext.getResources().getIdentifier("unitySurfaceView", "id", this.mContext.getPackageName()));
        if (IsWindowTranslucent()) {
            surfaceView.getHolder().setFormat(-3);
            surfaceView.setZOrderOnTop(true);
        } else {
            surfaceView.getHolder().setFormat(-1);
        }
        surfaceView.getHolder().addCallback(new UnityPlayer$19(this));
        surfaceView.setFocusable(true);
        surfaceView.setFocusableInTouchMode(true);
        return surfaceView;
    }

    private void DisableStaticSplashScreen() {
        runOnUiThread(new UnityPlayer$18(this));
    }

    private void EarlyEnableFullScreenIfEnabled(Activity activity) {
        View decorView;
        if (activity == null || activity.getWindow() == null) {
            return;
        }
        if ((getLaunchFullscreen() || activity.getIntent().getBooleanExtra("android.intent.extra.VR_LAUNCH", false)) && (decorView = this.mActivity.getWindow().getDecorView()) != null) {
            decorView.setSystemUiVisibility(7);
        }
    }

    private String GetGlViewContentDescription(Context context) {
        return context.getResources().getString(context.getResources().getIdentifier("game_view_content_description", "string", context.getPackageName()));
    }

    private boolean IsWindowTranslucent() {
        Activity activity = this.mActivity;
        if (activity == null) {
            return false;
        }
        TypedArray typedArrayObtainStyledAttributes = activity.getTheme().obtainStyledAttributes(new int[]{16842840});
        boolean z = typedArrayObtainStyledAttributes.getBoolean(0, false);
        typedArrayObtainStyledAttributes.recycle();
        return z;
    }

    public static void UnitySendMessage(String str, String str2, String str3) {
        if (m.c()) {
            try {
                nativeUnitySendMessage(str, str2, str3.getBytes("UTF-8"));
                return;
            } catch (UnsupportedEncodingException unused) {
                return;
            }
        }
        f.Log(5, "Native libraries not loaded - dropping message for " + str + "." + str2);
    }

    static void access$000(UnityPlayer unityPlayer, boolean z) {
        unityPlayer.nativeFocusChanged(z);
    }

    static boolean access$100(UnityPlayer unityPlayer) {
        return unityPlayer.getSplashEnabled();
    }

    static j access$1000(UnityPlayer unityPlayer) {
        return unityPlayer.m_SplashScreen;
    }

    static j access$1002(UnityPlayer unityPlayer, j jVar) {
        unityPlayer.m_SplashScreen = jVar;
        return jVar;
    }

    static void access$1100(UnityPlayer unityPlayer, int i, Surface surface) {
        unityPlayer.updateGLDisplay(i, surface);
    }

    static h access$1200(UnityPlayer unityPlayer) {
        return unityPlayer.m_PersistentUnitySurface;
    }

    static void access$1300(UnityPlayer unityPlayer) {
        unityPlayer.sendSurfaceChangedEvent();
    }

    static SurfaceView access$1400(UnityPlayer unityPlayer) {
        return unityPlayer.mGlView;
    }

    static void access$1500(UnityPlayer unityPlayer) {
        unityPlayer.nativeSendSurfaceChangedEvent();
    }

    static void access$1600(UnityPlayer unityPlayer, int i, Surface surface) {
        unityPlayer.nativeRecreateGfxState(i, surface);
    }

    static boolean access$1700(UnityPlayer unityPlayer) {
        return unityPlayer.mMainDisplayOverride;
    }

    static void access$1800(UnityPlayer unityPlayer) {
        unityPlayer.shutdown();
    }

    static boolean access$1900(UnityPlayer unityPlayer) {
        return unityPlayer.nativePause();
    }

    static void access$200(UnityPlayer unityPlayer) {
        unityPlayer.DisableStaticSplashScreen();
    }

    static boolean access$2002(UnityPlayer unityPlayer, boolean z) {
        unityPlayer.mQuitting = z;
        return z;
    }

    static void access$2100(UnityPlayer unityPlayer) {
        unityPlayer.nativeLowMemory();
    }

    static void access$2200(UnityPlayer unityPlayer) {
        unityPlayer.nativeResume();
    }

    static Context access$2300(UnityPlayer unityPlayer) {
        return unityPlayer.mContext;
    }

    static void access$2400(UnityPlayer unityPlayer) {
        unityPlayer.nativeSoftInputLostFocus();
    }

    static void access$2500(UnityPlayer unityPlayer) {
        unityPlayer.nativeReportKeyboardConfigChanged();
    }

    static void access$2700(UnityPlayer unityPlayer) {
        unityPlayer.nativeSoftInputCanceled();
    }

    static void access$2800(UnityPlayer unityPlayer, String str) {
        unityPlayer.nativeSetInputString(str);
    }

    static void access$2900(UnityPlayer unityPlayer) {
        unityPlayer.nativeSoftInputClosed();
    }

    static boolean access$300(UnityPlayer unityPlayer) {
        return unityPlayer.nativeRender();
    }

    static void access$3000(UnityPlayer unityPlayer, int i, int i2) {
        unityPlayer.nativeSetInputSelection(i, i2);
    }

    static void access$3100(UnityPlayer unityPlayer, int i, int i2, int i3, int i4) {
        unityPlayer.nativeSetInputArea(i, i2, i3, i4);
    }

    static void access$3200(UnityPlayer unityPlayer, boolean z) {
        unityPlayer.nativeSetKeyboardIsVisible(z);
    }

    static o access$3302(UnityPlayer unityPlayer, o oVar) {
        unityPlayer.mVideoPlayerProxy = oVar;
        return oVar;
    }

    static boolean access$3400(UnityPlayer unityPlayer) {
        return unityPlayer.nativeIsAutorotationOn();
    }

    static Activity access$3500(UnityPlayer unityPlayer) {
        return unityPlayer.mActivity;
    }

    static int access$3600(UnityPlayer unityPlayer) {
        return unityPlayer.mInitialScreenOrientation;
    }

    static IUnityPlayerLifecycleEvents access$3700(UnityPlayer unityPlayer) {
        return unityPlayer.m_UnityPlayerLifecycleEvents;
    }

    static int access$3800(UnityPlayer unityPlayer) {
        return unityPlayer.mNaturalOrientation;
    }

    static void access$400(UnityPlayer unityPlayer) {
        unityPlayer.finish();
    }

    static void access$500(UnityPlayer unityPlayer, String str) {
        unityPlayer.nativeSetLaunchURL(str);
    }

    static void access$600(UnityPlayer unityPlayer, int i, int i2) {
        unityPlayer.nativeOrientationChanged(i, i2);
    }

    static void access$800(UnityPlayer unityPlayer, boolean z) {
        unityPlayer.nativeMuteMasterAudio(z);
    }

    private void checkResumePlayer() {
        Activity activity = this.mActivity;
        if (this.mState.e(activity != null ? MultiWindowSupport.getAllowResizableWindow(activity) : false)) {
            this.mState.d(true);
            queueGLThreadEvent(new UnityPlayer$3(this));
            this.m_MainThread.b();
        }
    }

    private void finish() {
        Activity activity = this.mActivity;
        if (activity == null || activity.isFinishing()) {
            return;
        }
        this.mActivity.finish();
    }

    private boolean getARCoreEnabled() {
        try {
            return getApplicationInfo().metaData.getBoolean("unity.arcore-enable");
        } catch (Exception unused) {
            return false;
        }
    }

    private ApplicationInfo getApplicationInfo() {
        return this.mContext.getPackageManager().getApplicationInfo(this.mContext.getPackageName(), 128);
    }

    private boolean getLaunchFullscreen() {
        try {
            return getApplicationInfo().metaData.getBoolean("unity.launch-fullscreen");
        } catch (Exception unused) {
            return false;
        }
    }

    private int getNaturalOrientation(int i) {
        int rotation = ((WindowManager) this.mContext.getSystemService("window")).getDefaultDisplay().getRotation();
        if ((rotation == 0 || rotation == 2) && i == 2) {
            return 0;
        }
        return ((rotation == 1 || rotation == 3) && i == 1) ? 0 : 1;
    }

    private String getProcessName() {
        int iMyPid = Process.myPid();
        List<ActivityManager$RunningAppProcessInfo> runningAppProcesses = ((ActivityManager) this.mContext.getSystemService("activity")).getRunningAppProcesses();
        if (runningAppProcesses == null) {
            return null;
        }
        for (ActivityManager$RunningAppProcessInfo activityManager$RunningAppProcessInfo : runningAppProcesses) {
            if (activityManager$RunningAppProcessInfo.pid == iMyPid) {
                return activityManager$RunningAppProcessInfo.processName;
            }
        }
        return null;
    }

    private boolean getSplashEnabled() {
        try {
            return getApplicationInfo().metaData.getBoolean("unity.splash-enable");
        } catch (Exception unused) {
            return false;
        }
    }

    private static String getUnityNativeLibraryPath(Context context) {
        return context.getApplicationInfo().nativeLibraryDir;
    }

    private void hideStatusBar() {
        Activity activity = this.mActivity;
        if (activity != null) {
            activity.getWindow().setFlags(1024, 1024);
        }
    }

    private final native void initJni(Context context);

    private static String loadNative(String str) {
        String str2 = str + "/libmain.so";
        try {
            try {
                try {
                    System.load(str2);
                } catch (UnsatisfiedLinkError unused) {
                    System.loadLibrary("main");
                }
                str2 = m_InstantGameName;
                if (str2 != null) {
                    str = m_AndroidFilesDir + "/UnityPlayers/" + m_InstantGameEngine;
                }
                if (NativeLoader.load(str)) {
                    m.a();
                    return "";
                }
                f.Log(6, "NativeLoader.load failure, Unity libraries were not loaded.");
                return "NativeLoader.load failure, Unity libraries were not loaded.";
            } catch (UnsatisfiedLinkError e) {
                return logLoadLibMainError(str2, e.toString());
            }
        } catch (SecurityException e2) {
            return logLoadLibMainError(str2, e2.toString());
        }
    }

    private static String logLoadLibMainError(String str, String str2) {
        String str3 = "Failed to load 'libmain.so'\n\n" + str2;
        f.Log(6, str3);
        return str3;
    }

    private final native void nativeApplicationUnload();

    private final native boolean nativeDone();

    private final native void nativeFocusChanged(boolean z);

    private final native boolean nativeInjectEvent(InputEvent inputEvent);

    private final native boolean nativeIsAutorotationOn();

    private final native void nativeLowMemory();

    private final native void nativeMuteMasterAudio(boolean z);

    private final native void nativeOrientationChanged(int i, int i2);

    private final native boolean nativePause();

    private final native void nativeRecreateGfxState(int i, Surface surface);

    private final native boolean nativeRender();

    private final native void nativeReportKeyboardConfigChanged();

    private final native void nativeRestartActivityIndicator();

    private final native void nativeResume();

    private final native void nativeSendSurfaceChangedEvent();

    private final native void nativeSetInputArea(int i, int i2, int i3, int i4);

    private final native void nativeSetInputSelection(int i, int i2);

    private final native void nativeSetInputString(String str);

    private final native void nativeSetKeyboardIsVisible(boolean z);

    private final native void nativeSetLaunchURL(String str);

    private final native void nativeSoftInputCanceled();

    private final native void nativeSoftInputClosed();

    private final native void nativeSoftInputLostFocus();

    private static native void nativeUnitySendMessage(String str, String str2, byte[] bArr);

    private void pauseUnity() {
        reportSoftInputStr(null, 1, true);
        if (this.mState.f()) {
            if (m.c()) {
                Semaphore semaphore = new Semaphore(0);
                this.m_MainThread.a(isFinishing() ? new UnityPlayer$23(this, semaphore) : new UnityPlayer$24(this, semaphore));
                try {
                    if (!semaphore.tryAcquire(4L, TimeUnit.SECONDS)) {
                        f.Log(5, "Timeout while trying to pause the Unity Engine.");
                    }
                } catch (InterruptedException unused) {
                    f.Log(5, "UI thread got interrupted while trying to pause the Unity Engine.");
                }
                if (semaphore.drainPermits() > 0) {
                    destroy();
                }
            }
            this.mState.d(false);
            this.mState.b(true);
            if (this.m_AddPhoneCallListener) {
                this.m_TelephonyManager.listen(this.m_PhoneCallListener, 0);
            }
        }
    }

    private void queueGLThreadEvent(UnityPlayer$f unityPlayer$f) {
        if (isFinishing()) {
            return;
        }
        queueGLThreadEvent((Runnable) unityPlayer$f);
    }

    private void sendSurfaceChangedEvent() {
        if (m.c() && this.mState.e()) {
            this.m_MainThread.d(new UnityPlayer$20(this));
        }
    }

    private void shutdown() {
        this.mProcessKillRequested = nativeDone();
        this.mState.c(false);
    }

    private void swapViews(View view, View view2) {
        boolean z;
        if (this.mState.d()) {
            z = false;
        } else {
            pause();
            z = true;
        }
        if (view != null) {
            ViewParent parent = view.getParent();
            if (!(parent instanceof UnityPlayer) || ((UnityPlayer) parent) != this) {
                if (parent instanceof ViewGroup) {
                    ((ViewGroup) parent).removeView(view);
                }
                addView(view);
                bringChildToFront(view);
                view.setVisibility(0);
            }
        }
        if (view2 != null && view2.getParent() == this) {
            view2.setVisibility(8);
            removeView(view2);
        }
        if (z) {
            resume();
        }
    }

    private static void unloadNative() {
        if (m.c()) {
            if (!NativeLoader.unload()) {
                throw new UnsatisfiedLinkError("Unable to unload libraries from libmain.so");
            }
            m.b();
        }
    }

    private boolean updateDisplayInternal(int i, Surface surface) {
        if (!m.c() || !this.mState.e()) {
            return false;
        }
        Semaphore semaphore = new Semaphore(0);
        UnityPlayer$21 unityPlayer$21 = new UnityPlayer$21(this, i, surface, semaphore);
        if (i == 0) {
            UnityPlayer$e unityPlayer$e = this.m_MainThread;
            if (surface == null) {
                unityPlayer$e.b(unityPlayer$21);
            } else {
                unityPlayer$e.c(unityPlayer$21);
            }
        } else {
            unityPlayer$21.run();
        }
        if (surface != null || i != 0) {
            return true;
        }
        try {
            if (semaphore.tryAcquire(4L, TimeUnit.SECONDS)) {
                return true;
            }
            f.Log(5, "Timeout while trying detaching primary window.");
            return true;
        } catch (InterruptedException unused) {
            f.Log(5, "UI thread got interrupted while trying to detach the primary window from the Unity Engine.");
            return true;
        }
    }

    private void updateGLDisplay(int i, Surface surface) {
        if (this.mMainDisplayOverride) {
            return;
        }
        updateDisplayInternal(i, surface);
    }

    protected void addPhoneCallListener() {
        this.m_AddPhoneCallListener = true;
        this.m_TelephonyManager.listen(this.m_PhoneCallListener, 32);
    }

    public boolean addViewToPlayer(View view, boolean z) {
        swapViews(view, z ? this.mGlView : null);
        boolean z2 = true;
        boolean z3 = view.getParent() == this;
        boolean z4 = z && this.mGlView.getParent() == null;
        boolean z5 = this.mGlView.getParent() == this;
        if (!z3 || (!z4 && !z5)) {
            z2 = false;
        }
        if (!z2) {
            if (!z3) {
                f.Log(6, "addViewToPlayer: Failure adding view to hierarchy");
            }
            if (!z4 && !z5) {
                f.Log(6, "addViewToPlayer: Failure removing old view from hierarchy");
            }
        }
        return z2;
    }

    public void configurationChanged(Configuration configuration) {
        SurfaceView surfaceView = this.mGlView;
        if (surfaceView instanceof SurfaceView) {
            surfaceView.getHolder().setSizeFromLayout();
        }
        o oVar = this.mVideoPlayerProxy;
        if (oVar != null) {
            oVar.c();
        }
    }

    public void destroy() {
        h hVar = this.m_PersistentUnitySurface;
        if (hVar != null) {
            hVar.a();
            this.m_PersistentUnitySurface = null;
        }
        Camera2Wrapper camera2Wrapper = this.m_Camera2Wrapper;
        if (camera2Wrapper != null) {
            camera2Wrapper.a();
            this.m_Camera2Wrapper = null;
        }
        HFPStatus hFPStatus = this.m_HFPStatus;
        if (hFPStatus != null) {
            hFPStatus.a();
            this.m_HFPStatus = null;
        }
        NetworkConnectivity networkConnectivity = this.m_NetworkConnectivity;
        if (networkConnectivity != null) {
            networkConnectivity.b();
            this.m_NetworkConnectivity = null;
        }
        this.mQuitting = true;
        if (!this.mState.d()) {
            pause();
        }
        this.m_MainThread.a();
        try {
            this.m_MainThread.join(4000L);
        } catch (InterruptedException unused) {
            this.m_MainThread.interrupt();
        }
        BroadcastReceiver broadcastReceiver = this.mKillingIsMyBusiness;
        if (broadcastReceiver != null) {
            this.mContext.unregisterReceiver(broadcastReceiver);
        }
        this.mKillingIsMyBusiness = null;
        if (m.c()) {
            removeAllViews();
        }
        if (this.mProcessKillRequested) {
            this.m_UnityPlayerLifecycleEvents.onUnityPlayerQuitted();
            kill();
        }
        unloadNative();
    }

    protected void disableLogger() {
        f.a = true;
    }

    public boolean displayChanged(int i, Surface surface) {
        if (i == 0) {
            this.mMainDisplayOverride = surface != null;
            runOnUiThread(new UnityPlayer$22(this));
        }
        return updateDisplayInternal(i, surface);
    }

    protected void executeGLThreadJobs() {
        while (true) {
            Runnable runnable = (Runnable) this.m_Events.poll();
            if (runnable == null) {
                return;
            } else {
                runnable.run();
            }
        }
    }

    protected String getClipboardText() {
        ClipData primaryClip = this.m_ClipboardManager.getPrimaryClip();
        return primaryClip != null ? primaryClip.getItemAt(0).coerceToText(this.mContext).toString() : "";
    }

    protected String getKeyboardLayout() {
        i iVar = this.mSoftInputDialog;
        if (iVar == null) {
            return null;
        }
        return iVar.a();
    }

    protected String getLaunchURL() {
        Uri uri = this.m_launchUri;
        if (uri != null) {
            return uri.toString();
        }
        return null;
    }

    protected int getNetworkConnectivity() {
        if (!PlatformSupport.NOUGAT_SUPPORT) {
            return 0;
        }
        if (this.m_NetworkConnectivity == null) {
            this.m_NetworkConnectivity = new NetworkConnectivity(this.mContext);
        }
        return this.m_NetworkConnectivity.a();
    }

    public String getNetworkProxySettings(String str) {
        String str2;
        String str3;
        if (!str.startsWith("http:")) {
            if (str.startsWith("https:")) {
                str2 = "https.proxyHost";
                str3 = "https.proxyPort";
            }
            return null;
        }
        str2 = "http.proxyHost";
        str3 = "http.proxyPort";
        String property = System.getProperties().getProperty(str2);
        if (property != null && !"".equals(property)) {
            StringBuilder sb = new StringBuilder(property);
            String property2 = System.getProperties().getProperty(str3);
            if (property2 != null && !"".equals(property2)) {
                sb.append(":");
                sb.append(property2);
            }
            String property3 = System.getProperties().getProperty("http.nonProxyHosts");
            if (property3 != null && !"".equals(property3)) {
                sb.append('\n');
                sb.append(property3);
            }
            return sb.toString();
        }
        return null;
    }

    public Bundle getSettings() {
        return Bundle.EMPTY;
    }

    protected int getSplashMode() {
        try {
            return getApplicationInfo().metaData.getInt("unity.splash-mode");
        } catch (Exception unused) {
            return 0;
        }
    }

    protected int getUaaLLaunchProcessType() {
        String processName = getProcessName();
        return (processName == null || processName.equals(this.mContext.getPackageName())) ? 0 : 1;
    }

    public View getView() {
        return this;
    }

    protected void hideSoftInput() {
        postOnUiThread(new UnityPlayer$5(this));
    }

    public void init(int i, boolean z) {
    }

    protected boolean initializeGoogleAr() {
        if (this.m_ARCoreApi != null || this.mActivity == null || !getARCoreEnabled()) {
            return false;
        }
        GoogleARCoreApi googleARCoreApi = new GoogleARCoreApi();
        this.m_ARCoreApi = googleARCoreApi;
        googleARCoreApi.initializeARCore(this.mActivity);
        if (this.mState.d()) {
            return false;
        }
        this.m_ARCoreApi.resumeARCore();
        return false;
    }

    public boolean injectEvent(InputEvent inputEvent) {
        if (m.c()) {
            return nativeInjectEvent(inputEvent);
        }
        return false;
    }

    protected boolean isFinishing() {
        if (this.mQuitting) {
            return true;
        }
        Activity activity = this.mActivity;
        if (activity != null) {
            this.mQuitting = activity.isFinishing();
        }
        return this.mQuitting;
    }

    protected boolean isUaaLUseCase() {
        String callingPackage;
        Activity activity = this.mActivity;
        return (activity == null || (callingPackage = activity.getCallingPackage()) == null || !callingPackage.equals(this.mContext.getPackageName())) ? false : true;
    }

    protected void kill() {
        Process.killProcess(Process.myPid());
    }

    protected boolean loadLibrary(String str) {
        try {
            System.loadLibrary(str);
            return true;
        } catch (Exception | UnsatisfiedLinkError unused) {
            return false;
        }
    }

    public void lowMemory() {
        if (m.c()) {
            queueGLThreadEvent(new UnityPlayer$2(this));
        }
    }

    public void newIntent(Intent intent) {
        this.m_launchUri = intent.getData();
        this.m_MainThread.e();
    }

    @Override
    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    @Override
    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override
    public boolean onKeyLongPress(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override
    public boolean onKeyMultiple(int i, int i2, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override
    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override
    public boolean onTouchEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    @Override
    public void onUnityPlayerQuitted() {
    }

    @Override
    public void onUnityPlayerUnloaded() {
    }

    public void pause() {
        GoogleARCoreApi googleARCoreApi = this.m_ARCoreApi;
        if (googleARCoreApi != null) {
            googleARCoreApi.pauseARCore();
        }
        o oVar = this.mVideoPlayerProxy;
        if (oVar != null) {
            oVar.a();
        }
        AudioVolumeHandler audioVolumeHandler = this.m_AudioVolumeHandler;
        if (audioVolumeHandler != null) {
            audioVolumeHandler.a();
            this.m_AudioVolumeHandler = null;
        }
        OrientationLockListener orientationLockListener = this.m_OrientationLockListener;
        if (orientationLockListener != null) {
            orientationLockListener.a();
            this.m_OrientationLockListener = null;
        }
        pauseUnity();
    }

    protected void pauseJavaAndCallUnloadCallback() {
        runOnUiThread(new UnityPlayer$16(this));
    }

    void postOnUiThread(Runnable runnable) {
        new Handler(Looper.getMainLooper()).post(runnable);
    }

    void queueGLThreadEvent(Runnable runnable) {
        if (m.c()) {
            if (Thread.currentThread() == this.m_MainThread) {
                runnable.run();
            } else {
                this.m_Events.add(runnable);
            }
        }
    }

    public void quit() {
        destroy();
    }

    public void removeViewFromPlayer(View view) {
        swapViews(this.mGlView, view);
        boolean z = view.getParent() == null;
        boolean z2 = this.mGlView.getParent() == this;
        if (z && z2) {
            return;
        }
        if (!z) {
            f.Log(6, "removeViewFromPlayer: Failure removing view from hierarchy");
        }
        if (z2) {
            return;
        }
        f.Log(6, "removeVireFromPlayer: Failure agging old view to hierarchy");
    }

    public void reportError(String str, String str2) {
        f.Log(6, str + ": " + str2);
    }

    protected void reportSoftInputArea(Rect rect) {
        queueGLThreadEvent((UnityPlayer$f) new UnityPlayer$12(this, rect));
    }

    protected void reportSoftInputIsVisible(boolean z) {
        queueGLThreadEvent((UnityPlayer$f) new UnityPlayer$13(this, z));
    }

    protected void reportSoftInputSelection(int i, int i2) {
        queueGLThreadEvent((UnityPlayer$f) new UnityPlayer$11(this, i, i2));
    }

    protected void reportSoftInputStr(String str, int i, boolean z) {
        if (i == 1) {
            hideSoftInput();
        }
        queueGLThreadEvent((UnityPlayer$f) new UnityPlayer$10(this, z, str, i));
    }

    protected void requestUserAuthorization(String str) {
        if (str == null || str.isEmpty() || this.mActivity == null) {
            return;
        }
        UnityPermissions$ModalWaitForPermissionResponse unityPermissions$ModalWaitForPermissionResponse = new UnityPermissions$ModalWaitForPermissionResponse();
        UnityPermissions.requestUserPermissions(this.mActivity, new String[]{str}, unityPermissions$ModalWaitForPermissionResponse);
        unityPermissions$ModalWaitForPermissionResponse.waitForResponse();
    }

    public void resume() {
        GoogleARCoreApi googleARCoreApi = this.m_ARCoreApi;
        if (googleARCoreApi != null) {
            googleARCoreApi.resumeARCore();
        }
        this.mState.b(false);
        o oVar = this.mVideoPlayerProxy;
        if (oVar != null) {
            oVar.b();
        }
        checkResumePlayer();
        if (m.c()) {
            nativeRestartActivityIndicator();
        }
        if (this.m_AudioVolumeHandler == null) {
            this.m_AudioVolumeHandler = new AudioVolumeHandler(this.mContext);
        }
        if (this.m_OrientationLockListener == null && m.c()) {
            this.m_OrientationLockListener = new OrientationLockListener(this.mContext);
        }
    }

    void runOnAnonymousThread(Runnable runnable) {
        new Thread(runnable).start();
    }

    void runOnUiThread(Runnable runnable) {
        Activity activity = this.mActivity;
        if (activity != null) {
            activity.runOnUiThread(runnable);
        } else if (Thread.currentThread() != Looper.getMainLooper().getThread()) {
            this.mHandler.post(runnable);
        } else {
            runnable.run();
        }
    }

    protected void setCharacterLimit(int i) {
        runOnUiThread(new UnityPlayer$7(this, i));
    }

    protected void setClipboardText(String str) {
        this.m_ClipboardManager.setPrimaryClip(ClipData.newPlainText("Text", str));
    }

    protected void setHideInputField(boolean z) {
        runOnUiThread(new UnityPlayer$8(this, z));
    }

    protected void setSelection(int i, int i2) {
        runOnUiThread(new UnityPlayer$9(this, i, i2));
    }

    protected void setSoftInputStr(String str) {
        runOnUiThread(new UnityPlayer$6(this, str));
    }

    protected void showSoftInput(String str, int i, boolean z, boolean z2, boolean z3, boolean z4, String str2, int i2, boolean z5, boolean z6) {
        postOnUiThread(new UnityPlayer$4(this, this, str, i, z, z2, z3, z4, str2, i2, z5, z6));
    }

    protected boolean showVideoPlayer(String str, int i, int i2, int i3, boolean z, int i4, int i5) {
        if (this.mVideoPlayerProxy == null) {
            this.mVideoPlayerProxy = new o(this);
        }
        boolean zA = this.mVideoPlayerProxy.a(this.mContext, str, i, i2, i3, z, i4, i5, new UnityPlayer$14(this));
        if (zA) {
            runOnUiThread(new UnityPlayer$15(this));
        }
        return zA;
    }

    protected boolean skipPermissionsDialog() {
        Activity activity = this.mActivity;
        if (activity != null) {
            return UnityPermissions.skipPermissionsDialog(activity);
        }
        return false;
    }

    public boolean startOrientationListener(int i) {
        String str;
        if (this.mOrientationListener != null) {
            str = "Orientation Listener already started.";
        } else {
            UnityPlayer$17 unityPlayer$17 = new UnityPlayer$17(this, this.mContext, i);
            this.mOrientationListener = unityPlayer$17;
            if (unityPlayer$17.canDetectOrientation()) {
                this.mOrientationListener.enable();
                return true;
            }
            str = "Orientation Listener cannot detect orientation.";
        }
        f.Log(5, str);
        return false;
    }

    public boolean stopOrientationListener() {
        OrientationEventListener orientationEventListener = this.mOrientationListener;
        if (orientationEventListener == null) {
            f.Log(5, "Orientation Listener was not started.");
            return false;
        }
        orientationEventListener.disable();
        this.mOrientationListener = null;
        return true;
    }

    protected void toggleGyroscopeSensor(boolean z) {
        SensorManager sensorManager = (SensorManager) this.mContext.getSystemService("sensor");
        Sensor defaultSensor = sensorManager.getDefaultSensor(11);
        if (z) {
            sensorManager.registerListener(this.m_FakeListener, defaultSensor, 1);
        } else {
            sensorManager.unregisterListener(this.m_FakeListener);
        }
    }

    public void unload() {
        nativeApplicationUnload();
    }

    public void windowFocusChanged(boolean z) {
        this.mState.a(z);
        if (this.mState.e()) {
            i iVar = this.mSoftInputDialog;
            if (iVar == null || iVar.a) {
                if (z) {
                    this.m_MainThread.c();
                } else {
                    this.m_MainThread.d();
                }
                checkResumePlayer();
            }
        }
    }
}
