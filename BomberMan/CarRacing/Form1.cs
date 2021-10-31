using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;


namespace CarRacing
{
    public partial class Form1 : Form
    {
        int explosion_numbers;

        Bitmap bmp_user1;
        Bitmap bmp_user2;
        Bitmap bmp_Explosion;
        Bitmap bmp_Bumb;
        Bitmap bmp_Wall;
        Bitmap bmp_Woodbox;
        Bitmap bmp_Item1;
        Bitmap bmp_Item2;

        int[] woodbox_Index;

        PictureBox[] arrMapPictureBox;

        int[] position_Function;
        List<int> wall_Index;

        Random rand = new Random();

        int moveDealy = 150;
        bool moveCheck = true;

        int collectedCoin = 0;

        public Form1()
        {
            InitializeComponent();
            front_User();
            set_default();
            input_tick();
        }

        void set_default()
        {
            try
            {
                // default exposion numbers
                explosion_numbers = 2;
                // user1 image
                bmp_user1 = new Bitmap(Resource1.uFront);
                User1.Image = (Image)bmp_user1;
                // user2 image
                bmp_user2 = new Bitmap(Resource1.User2);
                User2.Image = (Image)bmp_user2;
                // bomb image
                bmp_Bumb = new Bitmap(Resource1.bo);
                // explosion iamage
                bmp_Explosion = new Bitmap(Resource1.ex);
                // set wall image
                bmp_Wall = new Bitmap(Resource1.tiles_wall);
                // set woodbox image
                bmp_Woodbox = new Bitmap(Resource1.tiles_wood);
                // set itemj image
                bmp_Item1 = new Bitmap(Resource1.item1);
                bmp_Item2 = new Bitmap(Resource1.item2);

                arrMapPictureBox = new PictureBox[225]
                {
                    x0y0, x1y0, x2y0, x3y0, x4y0, x5y0, x6y0, x7y0, x8y0, x9y0, x10y0, x11y0, x12y0, x13y0, x14y0,
                    x0y1, x1y1, x2y1, x3y1, x4y1, x5y1, x6y1, x7y1, x8y1, x9y1, x10y1, x11y1, x12y1, x13y1, x14y1,
                    x0y2, x1y2, x2y2, x3y2, x4y2, x5y2, x6y2, x7y2, x8y2, x9y2, x10y2, x11y2, x12y2, x13y2, x14y2,
                    x0y3, x1y3, x2y3, x3y3, x4y3, x5y3, x6y3, x7y3, x8y3, x9y3, x10y3, x11y3, x12y3, x13y3, x14y3,
                    x0y4, x1y4, x2y4, x3y4, x4y4, x5y4, x6y4, x7y4, x8y4, x9y4, x10y4, x11y4, x12y4, x13y4, x14y4,
                    x0y5, x1y5, x2y5, x3y5, x4y5, x5y5, x6y5, x7y5, x8y5, x9y5, x10y5, x11y5, x12y5, x13y5, x14y5,
                    x0y6, x1y6, x2y6, x3y6, x4y6, x5y6, x6y6, x7y6, x8y6, x9y6, x10y6, x11y6, x12y6, x13y6, x14y6,
                    x0y7, x1y7, x2y7, x3y7, x4y7, x5y7, x6y7, x7y7, x8y7, x9y7, x10y7, x11y7, x12y7, x13y7, x14y7,
                    x0y8, x1y8, x2y8, x3y8, x4y8, x5y8, x6y8, x7y8, x8y8, x9y8, x10y8, x11y8, x12y8, x13y8, x14y8,
                    x0y9, x1y9, x2y9, x3y9, x4y9, x5y9, x6y9, x7y9, x8y9, x9y9, x10y9, x11y9, x12y9, x13y9, x14y9,
                    x0y10, x1y10, x2y10, x3y10, x4y10, x5y10, x6y10, x7y10, x8y10, x9y10, x10y10, x11y10, x12y10, x13y10, x14y10,
                    x0y11, x1y11, x2y11, x3y11, x4y11, x5y11, x6y11, x7y11, x8y11, x9y11, x10y11, x11y11, x12y11, x13y11, x14y11,
                    x0y12, x1y12, x2y12, x3y12, x4y12, x5y12, x6y12, x7y12, x8y12, x9y12, x10y12, x11y12, x12y12, x13y12, x14y12,
                    x0y13, x1y13, x2y13, x3y13, x4y13, x5y13, x6y13, x7y13, x8y13, x9y13, x10y13, x11y13, x12y13, x13y13, x14y13,
                    x0y14, x1y14, x2y14, x3y14, x4y14, x5y14, x6y14, x7y14, x8y14, x9y14, x10y14, x11y14, x12y14, x13y14, x14y14
                };


                // 0 움직일 수 있음, 1 벽, 2 나무상자, 3 아이템, 4 아이템, 5 폭탄, 6 미사용(0대체 빈값) 11 플레이어1, 12 플레이어2
                position_Function = new int[225];

                // 벽 이미지 생성용 리스트
                wall_Index = new List<int>(new int[92]
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
                    15, 29,
                    30, 32, 34, 36, 38, 40, 42, 44,
                    45, 59,
                    60, 62, 64, 66, 68, 70, 72, 74,
                    75, 89,
                    90, 92, 94, 96, 98, 100, 102, 104,
                    105, 119,
                    120, 122, 124, 126, 128, 130, 132, 134,
                    135, 149,
                    150, 152, 154, 156, 158, 160, 162, 164,
                    165, 179,
                    180, 182, 184, 186, 188, 190, 192, 194,
                    195, 209,
                    210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224
                });

                // 나무 상자 이미지 생성용
                woodbox_Index = new int[30]
                {
                    33, 35, 37, 39, 41,
                    63, 65, 67, 69, 71,
                    93, 95, 97, 99, 101,
                    123, 125, 127, 129, 131,
                    153, 155, 157, 159, 161,
                    183, 185, 187, 189, 191
                };

                foreach (var indexer in wall_Index)
                {
                    // 해당 인덱스의 픽쳐 박스를 모두 벽 이미지 출력
                    arrMapPictureBox[indexer].Image = (Image)bmp_Wall;
                    // 벽으로 변경된 인덱스의 이동 가능을 불가능으로 변경
                    position_Function[indexer] = 1;
                }

                foreach (var indexer in woodbox_Index)
                {
                    // 해당 인덱스의 픽쳐 박스를 모두 벽 이미지 출력
                    arrMapPictureBox[indexer].Image = (Image)bmp_Woodbox;
                    // 나무상자로 변경된 인덱스의 이동 가능을 불가능으로 변경
                    position_Function[indexer] = 2;
                }
            }
            catch
            {

            }

            // 플레이어 1
            position_Function[196] = 11;

            // 플레이어 2
            position_Function[28] = 12;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (moveCheck)
            {
                moveCheck = false;

                // 현재 위치에 해당하는 객체의 인덱스를 받아온다.
                int index = check_Next_Object_Index(User1);
                // 현재 위치값에 대한 기능을 0으로 매핑한다.
                position_Function[index] = 0;

                if (e.KeyCode == Keys.Left)
                {

                    // 유저 위치를 왼쪽으로 -30 해주고
                    User1.Left += -30;
                
                    // 움직일 수 있는지 체크해서
                    int check = check_Key(User1);

                    // 벽, 나무상자 등으로 움직일 수 없다면, 
                    if (check == 1)
                    {
                        // 원래대로
                        User1.Left += 30;
                        position_Function[index] = 11;
                    }
                }
                
                if (e.KeyCode == Keys.Right)
                {
                    User1.Left += 30;

                    int check = check_Key(User1);

                    if (check == 1)
                    {
                        User1.Left += -30;
                        position_Function[index] = 11;
                    }
                }

                if (e.KeyCode == Keys.Up)
                {
                    User1.Top += -30;

                    int check = check_Key(User1);

                    if (check == 1)
                    {
                    
                        User1.Top += 30;
                        position_Function[index] = 11;
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    User1.Top += 30;

                    int check = check_Key(User1);

                    if (check == 1)
                    {
                        User1.Top += -30;
                        position_Function[index] = 11;
                    }
                }

            }

            if (e.KeyCode == Keys.Space)
            {
                int check = check_Key(User1);

                if (check == 11)
                {
                    show_Bomb();
                }
            }



        }

        int check_Key(PictureBox obj)
        {

            // 다음 이동 위치에 해당하는 객체의 인덱스를 받아온다.
            int index = check_Next_Object_Index(obj);

            // 그 객체의 위치에 어떤 기능이 가능한지 읽어들인다.
            int func_Num = position_Function[index];

            int check = 999;

            Console.WriteLine(func_Num);

            // 기능값이 0(빈공간) 이라면 이동 가능
            if (func_Num == 0)
            {
                position_Function[index] = 11;
                check = 11;
            }
            // 1(벽) 이라면 벽에 부딪혀 제거된다.
            else if (func_Num == 1 || func_Num == 2)
            {
                check = 1;
            }
            else if (func_Num == 3)
            {
                check = 3;

                // 아이템 값인, 폭탄 범위 증가 +1
                explosion_numbers += 1;

                // Item1 이미지 부분을, 빈공간으로 변경 
                arrMapPictureBox[index].Image = null;

                // 연산 처리에 대한 부분은 빈공간 값인 0으로 변경
                position_Function[index] = 0;
            }
            else if (func_Num == 4)
            {
                check = 4;

                // Item1 이미지 부분을, 빈공간으로 변경 
                arrMapPictureBox[index].Image = null;

                // 연산 처리에 대한 부분은 빈공간 값인 0으로 변경
                position_Function[index] = 0;
            }

            return check;
        }

        void show_Bomb()
        {

            PictureBox picturebox_Bomb = new PictureBox();

            try
            {
                // 폭탄 설정

                picturebox_Bomb.Width = 30;
                picturebox_Bomb.Height = 30;
                picturebox_Bomb.Location = new Point(User1.Location.X, User1.Location.Y);
                picturebox_Bomb.SizeMode = PictureBoxSizeMode.StretchImage;
                picturebox_Bomb.Image = (Image)bmp_Bumb;

                int index = check_Next_Object_Index(User1);
                if (position_Function[index] == 11)
                {
                    // 폭탄 보여주기
                    this.Controls.Add(picturebox_Bomb);
                    picturebox_Bomb.BringToFront();
                    position_Function[index] = 5;

                    change_BombToExplosion(picturebox_Bomb, picturebox_Bomb.Location.X, picturebox_Bomb.Location.Y, index);
                }
                // 빈공간이 아니면, 폭탄 출력하지 않고 제거,
                else
                {
                    picturebox_Bomb.Dispose();
                }


            }
            catch
            {
                if (picturebox_Bomb != null)
                {
                    ((IDisposable)picturebox_Bomb).Dispose();
                }
            }
        }

        async void change_BombToExplosion(PictureBox p, int x, int y, int index)
        {
            await Task.Delay(1000); // 2초 
            p.Dispose();

            //right 각 폭발 증가 함수는 독립적으로 작용해야 한다.
            // 내부 반복되어야 한다. 
            show_center(x, y, index);
            show_right(x, y, explosion_numbers);
            show_left(x, y, explosion_numbers);
            show_top(x, y, explosion_numbers);
            show_bottom(x, y, explosion_numbers);

        }
        
        // 객체의 다음 진행 방향을 비교해서 이동시 해당하는 판단 기준을 반환
        private int check_Next_Object_Index(PictureBox obj)
        {
            int check = 999;
            int index = 0;

            // 매개변수로 받은 obj와
            // 맵의 모든 객체를 비교하면
            foreach (var allMap in arrMapPictureBox)
            {
                // 충돌된 객체에 해당하는 index 값를 알아낼 수 있고
                if (obj.Bounds.IntersectsWith(allMap.Bounds))
                {
                    // 이 인덱스는 해당 위치에서 발생해야 할 판단 기준을 가져와 반환한다.
                    // 0 이동 1 벽(못움직임) 2 나무박스 3 아이템 4 아이템 5  아이템 등등등 
                    check = index;
                    return check;
                }

                //picturebox foreach는 Linq indexing 불가능해서 인덱스를 체크한다.
                ++index;
            }
            return check;
        }

        private int explosion_Next_Function(PictureBox obj)
        {
            // 다음 폭발 위치에 해당하는 객체의 인덱스를 받아온다.
            int index = check_Next_Object_Index(obj);

            // 그 객체의 위치에 어떤 기능이 작동해야 하는지 읽어들인다.
            int func_Num = position_Function[index];

            // return
            int rtn = 999;

            // 기능값이 적이라면 코인+1
            if (func_Num == 12)
            {

                this.Controls.Add(obj);
                obj.BringToFront();
                remove_Picturebox(obj);

                collectedCoin++;
                label2.Text = "Coin = " + collectedCoin.ToString();
            }
            // 기능값이 본인이라면
            else if (func_Num == 11)
            {

                this.Controls.Add(obj);
                obj.BringToFront();
                remove_Picturebox(obj);

                foreach (var User1Index in position_Function)
                {
                    if (User1Index == 11)
                    {
                        position_Function[User1Index] = 0;
                    }
                }

                position_Function[196] = 11;
                User1.Location = new Point(x1y13.Location.X, x1y13.Location.Y);

            }
            // 기능값이 0(빈공간) 이라면 폭발 생성
            else if (func_Num == 0)
            {
                this.Controls.Add(obj);
                obj.BringToFront();
                remove_Picturebox(obj);
                return 0;
            }
            // 1(벽) 이라면 벽에 부딪혀 제거된다.
            else if (index == 1)
            {
                obj.Dispose();
                return 1;
            }
            // 나무상자:2 라면 랜덤하게 아이템3, 4로 변경후 출력하고 + 작동값도 추가한다.
            else if (func_Num == 2)
            {
                // 폭탄이 폭발되고, 사라지면,
                this.Controls.Add(obj);
                obj.BringToFront();
                remove_Picturebox(obj);

                int itemNum = rand.Next(0, 5);

                if (itemNum == 1 || itemNum == 2)
                {
                    // 바닦 이미지 부분을, Item1 으로 변경
                    arrMapPictureBox[index].Image = (Image)bmp_Item1;

                    // 연산 처리에 대한 부분은 Item1 값인 3으로 변경
                    position_Function[index] = 3;

                    return 2;
                    // 아이템은 일반 공간 picturebox 위에 새로 겹쳐져 생기면, 폭탄과는 다르게(생성 후 자동 제거 되므로)
                    // 굉장히 곤란하다. 아이템 획득시 객체를 제거하려면, 해당 객체에 접근하기 위해 객체를 따로 관리해야 한다.
                    // 차라리 원래 해당 위치의 이미지 출력 부분과 작동값만 변경한다.
                }
                else if (itemNum == 3)
                {
                    // 폭발 이미지였어야 하는 부분을, Item2으로 변경
                    arrMapPictureBox[index].Image = (Image)bmp_Item2;

                    // 연산 처리에 대한 부분은 Item2 값인 4로 변경
                    position_Function[index] = 4;

                    return 3;
                }
                // 아이템이 나오지 않는다면, 상자를 제거하고, 작동값을 빈공간으로 변경한다.
                else
                {
                    arrMapPictureBox[index].Image = null;
                    position_Function[index] = 0;
                    return 6;
                }
            }
            else if (func_Num == 3 || func_Num == 4)
            {
                // 폭탄이 폭발되고, 폭발이 사라지면,
                this.Controls.Add(obj);
                obj.BringToFront();
                remove_Picturebox(obj);

                //아이템도 제거되고, 작동값도 빈공간으로 변경한다.
                arrMapPictureBox[index].Image = null;
                position_Function[index] = 0;

                return 0;
            }

            return rtn;
        }


        // 센터는 그냥 터지면 된다.
        void show_center(int x, int y, int index)
        {
            // 터진 모습 설정
            PictureBox picturebox_Explosion = new PictureBox();
            pic_set(picturebox_Explosion);

            picturebox_Explosion.Location = new Point(x, y);

            explosion_Next_Function(picturebox_Explosion);
        }


        void show_right(int x, int y, int num)
        {
            int start = x + 30;
            int max = x + num * 30;

            // 폭발 1칸씩 증가는 의도한 방향으로 순차적으로 실행될 필요가 있다.
            for (int i = start; i < max; i+=30)
            {
                // 터진 모습 설정
                PictureBox picturebox_Explosion = new PictureBox();
                pic_set(picturebox_Explosion);
                
                // 위치 설정
                picturebox_Explosion.Location = new Point(i, y);
                // 해당 위치에 대한 판단
                int check = explosion_Next_Function(picturebox_Explosion);
                if (check != 0)
                    break;

            }        
        }

        void pic_set(PictureBox picturebox_Explosion)
        {
            picturebox_Explosion.Width = 30;
            picturebox_Explosion.Height = 30;
            picturebox_Explosion.SizeMode = PictureBoxSizeMode.StretchImage;
            picturebox_Explosion.Image = (Image)bmp_Explosion;
        }


        void show_left(int x, int y, int num)
        {
            int start = x - 30;
            int max = x - num * 30;

            // 폭발 1칸씩 증가는 의도한 방향으로 순차적으로 실행될 필요가 있다.
            for (int i = start; i > max; i -= 30)
            {
                // 터진 모습 설정
                PictureBox picturebox_Explosion = new PictureBox();
                pic_set(picturebox_Explosion);
                picturebox_Explosion.Location = new Point(i, y);

                // 해당 위치에 대한 판단
                int check = explosion_Next_Function(picturebox_Explosion);
                if (check != 0)
                    break;
            }
        }

        void show_bottom(int x, int y, int num)
        {
            int start = y + 30;
            int max = y + num * 30;

            // 폭발 1칸씩 증가는 의도한 방향으로 순차적으로 실행될 필요가 있다. parallel 안됨
            for (int i = start; i < max; i += 30)
            {
                // 터진 모습 설정
                PictureBox picturebox_Explosion = new PictureBox();
                pic_set(picturebox_Explosion);
                picturebox_Explosion.Location = new Point(x, i);

                // 해당 위치에 대한 판단
                int check = explosion_Next_Function(picturebox_Explosion);
                if (check != 0)
                    break;
            }
        }

        void show_top(int x, int y,  int num)
        {
            int start = y - 30;
            int max = y - num * 30;

            // 폭발 1칸씩 증가는 의도한 방향으로 순차적으로 실행될 필요가 있다. parallel 안됨
            for (int i = start; i > max; i -= 30)
            {
                // 터진 모습 설정
                PictureBox picturebox_Explosion = new PictureBox();
                pic_set(picturebox_Explosion);
                picturebox_Explosion.Location = new Point(x, i);

                // 해당 위치에 대한 판단
                int check = explosion_Next_Function(picturebox_Explosion);
                if (check != 0)
                    break;
            }
        }

        async void remove_Picturebox(PictureBox p)
        {
            await Task.Delay(1000); // 2초 
            p.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            User1.BringToFront();
        }

        void front_User()
        {
            Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            moveCheck = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //bombCheck = true;
        }

        void input_tick()
        {
            Timer timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = moveDealy;
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start();

            //Timer timer3 = new System.Windows.Forms.Timer();
            //timer3.Interval = bombDealy;
            //timer3.Tick += new EventHandler(timer3_Tick);

            //timer3.Start();
        }

    }

}
