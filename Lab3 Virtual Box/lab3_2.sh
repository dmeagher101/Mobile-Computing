mkdir -p ~/catkin_ws/src
cd ~/catkin_ws/src
cd ~/catkin_ws/
catkin_make
source devel/setup.bash
cd src
catkin_create_pkg beginner_tutorials std_msgs rospy roscpp
roscd beginner_tutorials
