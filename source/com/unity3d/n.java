package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.content.res.AssetFileDescriptor;
import android.media.MediaPlayer;
import android.media.MediaPlayer$OnBufferingUpdateListener;
import android.media.MediaPlayer$OnCompletionListener;
import android.media.MediaPlayer$OnPreparedListener;
import android.media.MediaPlayer$OnVideoSizeChangedListener;
import android.net.Uri;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Display;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder$Callback;
import android.view.SurfaceView;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.FrameLayout$LayoutParams;
import android.widget.MediaController;
import android.widget.MediaController$MediaPlayerControl;
import java.io.FileInputStream;
import java.io.IOException;

public final class n extends FrameLayout implements MediaPlayer$OnBufferingUpdateListener, MediaPlayer$OnCompletionListener, MediaPlayer$OnPreparedListener, MediaPlayer$OnVideoSizeChangedListener, SurfaceHolder$Callback, MediaController$MediaPlayerControl {
    private static boolean a = false;
    private final Context b;
    private final SurfaceView c;
    private final SurfaceHolder d;
    private final String e;
    private final int f;
    private final int g;
    private final boolean h;
    private final long i;
    private final long j;
    private final FrameLayout k;
    private final Display l;
    private int m;
    private int n;
    private int o;
    private int p;
    private MediaPlayer q;
    private MediaController r;
    private boolean s;
    private boolean t;
    private int u;
    private boolean v;
    private boolean w;
    private n$a x;
    private n$b y;
    private volatile int z;

    protected n(Context context, String str, int i, int i2, int i3, boolean z, long j, long j2, n$a n_a) {
        super(context);
        this.s = false;
        this.t = false;
        this.u = 0;
        this.v = false;
        this.w = false;
        this.z = 0;
        this.x = n_a;
        this.b = context;
        this.k = this;
        SurfaceView surfaceView = new SurfaceView(context);
        this.c = surfaceView;
        SurfaceHolder holder = surfaceView.getHolder();
        this.d = holder;
        holder.addCallback(this);
        this.k.setBackgroundColor(i);
        this.k.addView(this.c);
        this.l = ((WindowManager) this.b.getSystemService("window")).getDefaultDisplay();
        this.e = str;
        this.f = i2;
        this.g = i3;
        this.h = z;
        this.i = j;
        this.j = j2;
        if (a) {
            b("fileName: " + this.e);
        }
        if (a) {
            b("backgroundColor: " + i);
        }
        if (a) {
            b("controlMode: " + this.f);
        }
        if (a) {
            b("scalingMode: " + this.g);
        }
        if (a) {
            b("isURL: " + this.h);
        }
        if (a) {
            b("videoOffset: " + this.i);
        }
        if (a) {
            b("videoLength: " + this.j);
        }
        setFocusable(true);
        setFocusableInTouchMode(true);
    }

    private void a(int i) {
        this.z = i;
        n$a n_a = this.x;
        if (n_a != null) {
            n_a.a(this.z);
        }
    }

    static void a(String str) {
        b(str);
    }

    private static void b(String str) {
        Log.i("Video", "VideoPlayer: " + str);
    }

    static boolean b() {
        return a;
    }

    private void c() {
        FileInputStream fileInputStream;
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer != null) {
            mediaPlayer.setDisplay(this.d);
            if (this.v) {
                return;
            }
            if (a) {
                b("Resuming playback");
            }
            this.q.start();
            return;
        }
        a(0);
        doCleanUp();
        try {
            MediaPlayer mediaPlayer2 = new MediaPlayer();
            this.q = mediaPlayer2;
            if (this.h) {
                mediaPlayer2.setDataSource(this.b, Uri.parse(this.e));
            } else {
                if (this.j != 0) {
                    fileInputStream = new FileInputStream(this.e);
                    this.q.setDataSource(fileInputStream.getFD(), this.i, this.j);
                } else {
                    try {
                        AssetFileDescriptor assetFileDescriptorOpenFd = getResources().getAssets().openFd(this.e);
                        this.q.setDataSource(assetFileDescriptorOpenFd.getFileDescriptor(), assetFileDescriptorOpenFd.getStartOffset(), assetFileDescriptorOpenFd.getLength());
                        assetFileDescriptorOpenFd.close();
                    } catch (IOException unused) {
                        fileInputStream = new FileInputStream(this.e);
                        this.q.setDataSource(fileInputStream.getFD());
                        fileInputStream.close();
                    }
                }
                fileInputStream.close();
            }
            this.q.setDisplay(this.d);
            this.q.setScreenOnWhilePlaying(true);
            this.q.setOnBufferingUpdateListener(this);
            this.q.setOnCompletionListener(this);
            this.q.setOnPreparedListener(this);
            this.q.setOnVideoSizeChangedListener(this);
            this.q.setAudioStreamType(3);
            this.q.prepareAsync();
            this.y = new n$b(this, this);
            new Thread(this.y).start();
        } catch (Exception e) {
            if (a) {
                b("error: " + e.getMessage() + e);
            }
            a(2);
        }
    }

    private void d() {
        if (isPlaying()) {
            return;
        }
        a(1);
        if (a) {
            b("startVideoPlayback");
        }
        updateVideoLayout();
        if (this.v) {
            return;
        }
        start();
    }

    public final void CancelOnPrepare() {
        a(2);
    }

    final boolean a() {
        return this.v;
    }

    @Override
    public final boolean canPause() {
        return true;
    }

    @Override
    public final boolean canSeekBackward() {
        return true;
    }

    @Override
    public final boolean canSeekForward() {
        return true;
    }

    protected final void destroyPlayer() {
        if (a) {
            b("destroyPlayer");
        }
        if (!this.v) {
            pause();
        }
        doCleanUp();
    }

    protected final void doCleanUp() {
        n$b n_b = this.y;
        if (n_b != null) {
            n_b.a();
            this.y = null;
        }
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer != null) {
            mediaPlayer.release();
            this.q = null;
        }
        this.o = 0;
        this.p = 0;
        this.t = false;
        this.s = false;
    }

    @Override
    public final int getAudioSessionId() {
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return 0;
        }
        return mediaPlayer.getAudioSessionId();
    }

    @Override
    public final int getBufferPercentage() {
        if (this.h) {
            return this.u;
        }
        return 100;
    }

    @Override
    public final int getCurrentPosition() {
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return 0;
        }
        return mediaPlayer.getCurrentPosition();
    }

    @Override
    public final int getDuration() {
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return 0;
        }
        return mediaPlayer.getDuration();
    }

    @Override
    public final boolean isPlaying() {
        boolean z = this.t && this.s;
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return !z;
        }
        return mediaPlayer.isPlaying() || !z;
    }

    @Override
    public final void onBufferingUpdate(MediaPlayer mediaPlayer, int i) {
        if (a) {
            b("onBufferingUpdate percent:" + i);
        }
        this.u = i;
    }

    @Override
    public final void onCompletion(MediaPlayer mediaPlayer) {
        if (a) {
            b("onCompletion called");
        }
        destroyPlayer();
        a(3);
    }

    @Override
    public final boolean onKeyDown(int i, KeyEvent keyEvent) {
        if (i != 4 && (this.f != 2 || i == 0 || keyEvent.isSystem())) {
            MediaController mediaController = this.r;
            return mediaController != null ? mediaController.onKeyDown(i, keyEvent) : super.onKeyDown(i, keyEvent);
        }
        destroyPlayer();
        a(3);
        return true;
    }

    @Override
    public final void onPrepared(MediaPlayer mediaPlayer) {
        if (a) {
            b("onPrepared called");
        }
        n$b n_b = this.y;
        if (n_b != null) {
            n_b.a();
            this.y = null;
        }
        int i = this.f;
        if (i == 0 || i == 1) {
            MediaController mediaController = new MediaController(this.b);
            this.r = mediaController;
            mediaController.setMediaPlayer(this);
            this.r.setAnchorView(this);
            this.r.setEnabled(true);
            Context context = this.b;
            if (context instanceof Activity) {
                this.r.setSystemUiVisibility(((Activity) context).getWindow().getDecorView().getSystemUiVisibility());
            }
            this.r.show();
        }
        this.t = true;
        if (1 == 0 || !this.s) {
            return;
        }
        d();
    }

    @Override
    public final boolean onTouchEvent(MotionEvent motionEvent) {
        int action = motionEvent.getAction() & 255;
        if (this.f != 2 || action != 0) {
            MediaController mediaController = this.r;
            return mediaController != null ? mediaController.onTouchEvent(motionEvent) : super.onTouchEvent(motionEvent);
        }
        destroyPlayer();
        a(3);
        return true;
    }

    @Override
    public final void onVideoSizeChanged(MediaPlayer mediaPlayer, int i, int i2) {
        if (a) {
            b("onVideoSizeChanged called " + i + "x" + i2);
        }
        if (i != 0 && i2 != 0) {
            this.s = true;
            this.o = i;
            this.p = i2;
            if (!this.t || 1 == 0) {
                return;
            }
            d();
            return;
        }
        if (a) {
            b("invalid video width(" + i + ") or height(" + i2 + ")");
        }
    }

    @Override
    public final void pause() {
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return;
        }
        if (this.w) {
            mediaPlayer.pause();
        }
        this.v = true;
    }

    @Override
    public final void seekTo(int i) {
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return;
        }
        mediaPlayer.seekTo(i);
    }

    @Override
    public final void start() {
        if (a) {
            b("Start");
        }
        MediaPlayer mediaPlayer = this.q;
        if (mediaPlayer == null) {
            return;
        }
        if (this.w) {
            mediaPlayer.start();
        }
        this.v = false;
    }

    @Override
    public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
        if (a) {
            b("surfaceChanged called " + i + " " + i2 + "x" + i3);
        }
        if (this.m == i2 && this.n == i3) {
            return;
        }
        this.m = i2;
        this.n = i3;
        if (this.w) {
            updateVideoLayout();
        }
    }

    @Override
    public final void surfaceCreated(SurfaceHolder surfaceHolder) {
        if (a) {
            b("surfaceCreated called");
        }
        this.w = true;
        c();
    }

    @Override
    public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
        if (a) {
            b("surfaceDestroyed called");
        }
        this.w = false;
    }

    protected final void updateVideoLayout() {
        if (a) {
            b("updateVideoLayout");
        }
        if (this.q == null) {
            return;
        }
        if (this.m == 0 || this.n == 0) {
            WindowManager windowManager = (WindowManager) this.b.getSystemService("window");
            DisplayMetrics displayMetrics = new DisplayMetrics();
            windowManager.getDefaultDisplay().getMetrics(displayMetrics);
            this.m = displayMetrics.widthPixels;
            this.n = displayMetrics.heightPixels;
        }
        int i = this.m;
        int i2 = this.n;
        if (this.s) {
            int i3 = this.o;
            int i4 = this.p;
            float f = i3 / i4;
            float f2 = i / i2;
            int i5 = this.g;
            if (i5 == 1) {
                if (f2 <= f) {
                    i2 = (int) (i / f);
                } else {
                    i = (int) (i2 * f);
                }
            } else if (i5 == 2) {
                if (f2 >= f) {
                    i2 = (int) (i / f);
                } else {
                    i = (int) (i2 * f);
                }
            } else if (i5 == 0) {
                i = i3;
                i2 = i4;
            }
        } else if (a) {
            b("updateVideoLayout: Video size is not known yet");
        }
        if (this.m == i && this.n == i2) {
            return;
        }
        if (a) {
            b("frameWidth = " + i + "; frameHeight = " + i2);
        }
        this.k.updateViewLayout(this.c, new FrameLayout$LayoutParams(i, i2, 17));
    }
}
