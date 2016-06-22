<?php

class RegistrationTest extends PHPUnit_Framework_TestCase
{
    /**
     * @test
     */
    public function love_you()
    {
        $this->assertTrue(true);
    }

    /**
     * @test
     */
    public function registered_user_should_be_added_to_database()
    {
        $model = new \AllyTalks\WebApp\Model();

        $model->addUser(
            ['testlogin', 'testpassword', 'testnick', 'test@email.test']
        );
    }
}
