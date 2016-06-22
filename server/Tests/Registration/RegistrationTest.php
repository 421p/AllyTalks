<?php

class RegistrationTest extends PHPUnit_Framework_TestCase
{
    /**
     * @test
     */
    public function registered_user_should_be_added_to_database()
    {
        $model = new \AllyTalks\WebApp\Model();

        $model->addUser(
            [
                'login' => 'testlogin',
                'password' => 'testpassword',
                'nickname' => 'testnick',
                'email' => 'test@email.test',
            ]
        );

        $user = $model->getUserByLogin('testlogin');

        $this->assertEquals($user->getLogin(), 'testlogin');
        $this->assertEquals($user->getNickname(), 'testnick');
        $this->assertEquals($user->getEmail(), 'test@email.test');
        $this->assertTrue($user->verifyPassword('testpassword'));

        $model->deleteUser($user);
    }
}
